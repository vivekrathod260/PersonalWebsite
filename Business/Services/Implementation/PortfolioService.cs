using Business.DTOs;
using Data.Models;
using Data.Repositories;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Logging;

namespace Business.Services.Implementation;

public class PortfolioService : IPortfolioService
{
    private readonly IFirestoreRepository _repo;
    private readonly ILogger<PortfolioService> _logger;

    public PortfolioService(IFirestoreRepository repo, ILogger<PortfolioService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<ProfileDto?> GetProfileAsync()
    {
        var profile = await _repo.GetDocumentAsync<Profile>("profile", "main");
        if (profile == null) return null;

        return new ProfileDto
        {
            Name = profile.Name,
            Title = profile.Title,
            Intro = profile.Intro,
            About = profile.About,
            ImageUrl = profile.ImageUrl,
            ResumeUrl = profile.ResumeUrl,
            Email = profile.Email,
            Location = profile.Location,
            CtaPrimary = profile.CtaPrimary,
            CtaSecondary = profile.CtaSecondary
        };
    }

    public async Task<List<ProjectDto>> GetProjectsAsync()
    {
        var projects = await _repo.GetCollectionOrderedAsync<Project>("projects", "order");
        return projects.Where(p => p.Visible).Select(MapProject).ToList();
    }

    public async Task<List<ProjectDto>> GetFeaturedProjectsAsync()
    {
        var projects = await _repo.GetCollectionOrderedAsync<Project>("projects", "order");
        return projects.Where(p => p.Featured && p.Visible).Select(MapProject).ToList();
    }

    public async Task<List<ExperienceDto>> GetExperiencesAsync()
    {
        var experiences = await _repo.GetCollectionOrderedAsync<Experience>("experiences", "order");
        return experiences.Where(e => e.Visible).Select(e => new ExperienceDto
        {
            Id = e.Id,
            Company = e.Company,
            Role = e.Role,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Current = e.Current,
            Visible = e.Visible,
            ExperienceSkills = (e.ExperienceSkills ?? new List<ExperienceSkill>()).Select(es => new ExperienceSkillDto
            {
                Name = es.Name,
                IconUrl = es.IconUrl,
                Highlight = es.Highlight
            }).ToList()
        }).ToList();
    }

    public async Task<List<SkillCategoryDto>> GetSkillsGroupedAsync()
    {
        var skills = await _repo.GetCollectionOrderedAsync<Skill>("skills", "order");
        return skills.Where(s => s.Visible).GroupBy(s => s.Category)
            .Select(g => new SkillCategoryDto
            {
                Category = g.Key,
                Skills = g.Select(s => new SkillDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Category = s.Category,
                    IconUrl = s.IconUrl,
                    Proficiency = s.Proficiency,
                    Visible = s.Visible
                }).ToList()
            }).ToList();
    }

    public async Task<List<TestimonialDto>> GetTestimonialsAsync()
    {
        var testimonials = await _repo.GetCollectionOrderedAsync<Testimonial>("testimonials", "order");
        return testimonials.Where(t => t.Visible).Select(t => new TestimonialDto
        {
            Id = t.Id,
            Name = t.Name,
            Company = t.Company,
            Designation = t.Designation,
            Review = t.Review,
            ImageUrl = t.ImageUrl,
            Visible = t.Visible
        }).ToList();
    }

    public async Task<List<SocialDto>> GetSocialsAsync()
    {
        var socials = await _repo.GetCollectionOrderedAsync<Social>("socials", "order");
        return socials.Where(s => s.Visible).Select(s => new SocialDto
        {
            Id = s.Id,
            Platform = s.Platform,
            Url = s.Url,
            Icon = s.Icon,
            Visible = s.Visible
        }).ToList();
    }

    public async Task<SettingsDto?> GetSettingsAsync()
    {
        var settings = await _repo.GetDocumentAsync<Settings>("settings", "main");
        if (settings == null) return null;

        return new SettingsDto
        {
            ShowHero = settings.ShowHero,
            ShowAbout = settings.ShowAbout,
            ShowExperience = settings.ShowExperience,
            ShowProjects = settings.ShowProjects,
            ShowSkills = settings.ShowSkills,
            ShowTestimonials = settings.ShowTestimonials,
            ShowContact = settings.ShowContact,
            ShowFooter = settings.ShowFooter,
            ShowNavbar = settings.ShowNavbar
        };
    }

    public async Task SubmitContactAsync(ContactFormDto form)
    {
        var message = new ContactMessage
        {
            Name = form.Name,
            Email = form.Email,
            Subject = form.Subject,
            Message = form.Message,
            CreatedAt = Timestamp.FromDateTime(DateTime.UtcNow)
        };

        await _repo.AddDocumentAsync("contact", message);
        _logger.LogInformation("Contact form submitted from {Email}", form.Email);
    }

    private static ProjectDto MapProject(Project p) => new()
    {
        Id = p.Id,
        Title = p.Title,
        Description = p.Description,
        TechStack = p.TechStack,
        Images = p.Images,
        VideoUrl = p.VideoUrl,
        GithubUrl = p.GithubUrl,
        LiveUrl = p.LiveUrl,
        Tags = p.Tags,
        Featured = p.Featured,
        Visible = p.Visible
    };
}
