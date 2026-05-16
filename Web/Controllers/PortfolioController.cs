using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
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

    [HttpPost("contact")]
    public async Task<IActionResult> SubmitContact([FromBody] ContactFormDto form)
    {
        if (string.IsNullOrWhiteSpace(form.Name) ||
            string.IsNullOrWhiteSpace(form.Email) ||
            string.IsNullOrWhiteSpace(form.Message))
        {
            return BadRequest("Name, email, and message are required.");
        }

        // Basic server-side email format validation
        var emailPattern = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$");
        if (!emailPattern.IsMatch(form.Email))
        {
            return BadRequest("Invalid email address.");
        }

        await _portfolioService.SubmitContactAsync(form);
        return Ok(new { message = "Message sent successfully!" });
    }
}
