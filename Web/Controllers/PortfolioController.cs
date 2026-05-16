using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;
    private readonly IMemoryCache _memoryCache;

    public PortfolioController(IPortfolioService portfolioService, IMemoryCache memoryCache)
    {
        _portfolioService = portfolioService;
        _memoryCache = memoryCache;
    }

    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
    [HttpGet("profile")]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        var profile = await _portfolioService.GetProfileAsync();
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    [HttpGet("projects")]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects()
    {
        return Ok(await _portfolioService.GetProjectsAsync());
    }

    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    [HttpGet("projects/featured")]
    public async Task<ActionResult<List<ProjectDto>>> GetFeaturedProjects()
    {
        return Ok(await _portfolioService.GetFeaturedProjectsAsync());
    }

    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any)]
    [HttpGet("experiences")]
    public async Task<ActionResult<List<ExperienceDto>>> GetExperiences()
    {
        return Ok(await _portfolioService.GetExperiencesAsync());
    }

    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
    [HttpGet("skills")]
    public async Task<ActionResult<List<SkillCategoryDto>>> GetSkills()
    {
        return Ok(await _portfolioService.GetSkillsGroupedAsync());
    }

    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
    [HttpGet("testimonials")]
    public async Task<ActionResult<List<TestimonialDto>>> GetTestimonials()
    {
        return Ok(await _portfolioService.GetTestimonialsAsync());
    }

    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
    [HttpGet("socials")]
    public async Task<ActionResult<List<SocialDto>>> GetSocials()
    {
        return Ok(await _portfolioService.GetSocialsAsync());
    }

    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [HttpGet("settings")]
    public async Task<ActionResult<SettingsDto?>> GetSettings()
    {
        var settings = await _portfolioService.GetSettingsAsync();
        if (settings == null) return NotFound();
        return Ok(settings);
    }

    // Limit request body to 64KB for contact submissions as an extra protection
    [RequestSizeLimit(65536)]
    [HttpPost("contact")]
    public async Task<IActionResult> SubmitContact([FromBody] ContactFormDto form)
    {
        // Basic required fields
        if (string.IsNullOrWhiteSpace(form.Name) ||
            string.IsNullOrWhiteSpace(form.Email) ||
            string.IsNullOrWhiteSpace(form.Message))
        {
            return BadRequest("Name, email, and message are required.");
        }

        // Simple Content-Length check (if provided) to reject oversized payloads early
        const long MaxAllowedBytes = 64 * 1024; // 64 KB
        if (Request.ContentLength.HasValue && Request.ContentLength.Value > MaxAllowedBytes)
        {
            return StatusCode(StatusCodes.Status413PayloadTooLarge, "Payload too large.");
        }

        // Field length limits
        const int MaxNameLength = 100;
        const int MaxEmailLength = 254;
        const int MaxSubjectLength = 200;
        const int MaxMessageLength = 5000; // ~5 KB of text

        if (form.Name.Length > MaxNameLength) return BadRequest($"Name cannot exceed {MaxNameLength} characters.");
        if (form.Email.Length > MaxEmailLength) return BadRequest($"Email cannot exceed {MaxEmailLength} characters.");
        if (!Regex.IsMatch(form.Email, "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$")) return BadRequest("Invalid email address.");
        if (!string.IsNullOrEmpty(form.Subject) && form.Subject.Length > MaxSubjectLength) return BadRequest($"Subject cannot exceed {MaxSubjectLength} characters.");
        if (form.Message.Length > MaxMessageLength) return StatusCode(StatusCodes.Status413PayloadTooLarge, $"Message too long. Maximum {MaxMessageLength} characters allowed.");

        // Basic rate limiting per IP: max submissions per window
        try
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var cacheKey = $"contact_rate_{ip}";
            const int MaxRequestsPerWindow = 10;
            TimeSpan window = TimeSpan.FromMinutes(60);

            var counter = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = window;
                return 0;
            });

            if (counter >= MaxRequestsPerWindow)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, "Too many contact submissions. Please try again later.");
            }

            // increment
            _memoryCache.Set(cacheKey, counter + 1, DateTimeOffset.UtcNow.Add(window));
        }
        catch
        {
            // if caching fails for some reason, proceed without rate limiting
        }

        await _portfolioService.SubmitContactAsync(form);
        return Ok(new { message = "Message sent successfully!" });
    }
}
