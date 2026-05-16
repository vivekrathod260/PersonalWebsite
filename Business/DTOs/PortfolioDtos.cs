namespace Business.DTOs;

public class ProfileDto
{
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Intro { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string ResumeUrl { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string CtaPrimary { get; set; } = string.Empty;
    public string CtaSecondary { get; set; } = string.Empty;
}

public class ProjectDto
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> TechStack { get; set; } = [];
    public List<string> Images { get; set; } = [];
    public string VideoUrl { get; set; } = string.Empty;
    public string GithubUrl { get; set; } = string.Empty;
    public string LiveUrl { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public bool Featured { get; set; }
    public bool Visible { get; set; } = true;
}

public class ExperienceDto
{
    public string Id { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public bool Current { get; set; }
    public bool Visible { get; set; } = true;
}

public class SkillDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public int Proficiency { get; set; }
    public bool Visible { get; set; } = true;
}

public class TestimonialDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string Review { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool Visible { get; set; } = true;
}

public class SocialDto
{
    public string Id { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool Visible { get; set; } = true;
}

public class ContactFormDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class SkillCategoryDto
{
    public string Category { get; set; } = string.Empty;
    public List<SkillDto> Skills { get; set; } = [];
}

public class SettingsDto
{
    public bool ShowHero { get; set; } = true;
    public bool ShowAbout { get; set; } = true;
    public bool ShowExperience { get; set; } = true;
    public bool ShowProjects { get; set; } = true;
    public bool ShowSkills { get; set; } = true;
    public bool ShowTestimonials { get; set; } = true;
    public bool ShowContact { get; set; } = true;
    public bool ShowFooter { get; set; } = true;
    public bool ShowNavbar { get; set; } = true;
}
