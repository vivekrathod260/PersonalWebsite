using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("profile")]
    public async Task<ActionResult<ProfileDto>> GetProfile()
    {
        var profile = await _portfolioService.GetProfileAsync();
        if (profile == null) return NotFound();
        return Ok(profile);
    }

    [HttpGet("projects")]
    public async Task<ActionResult<List<ProjectDto>>> GetProjects()
    {
        return Ok(await _portfolioService.GetProjectsAsync());
    }

    [HttpGet("projects/featured")]
    public async Task<ActionResult<List<ProjectDto>>> GetFeaturedProjects()
    {
        return Ok(await _portfolioService.GetFeaturedProjectsAsync());
    }

    [HttpGet("experiences")]
    public async Task<ActionResult<List<ExperienceDto>>> GetExperiences()
    {
        return Ok(await _portfolioService.GetExperiencesAsync());
    }

    [HttpGet("skills")]
    public async Task<ActionResult<List<SkillCategoryDto>>> GetSkills()
    {
        return Ok(await _portfolioService.GetSkillsGroupedAsync());
    }

    [HttpGet("testimonials")]
    public async Task<ActionResult<List<TestimonialDto>>> GetTestimonials()
    {
        return Ok(await _portfolioService.GetTestimonialsAsync());
    }

    [HttpGet("socials")]
    public async Task<ActionResult<List<SocialDto>>> GetSocials()
    {
        return Ok(await _portfolioService.GetSocialsAsync());
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

        await _portfolioService.SubmitContactAsync(form);
        return Ok(new { message = "Message sent successfully!" });
    }
}
