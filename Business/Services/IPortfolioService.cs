using Business.DTOs;

namespace Business.Services;

public interface IPortfolioService
{
    Task<ProfileDto?> GetProfileAsync();
    Task<List<ProjectDto>> GetProjectsAsync();
    Task<List<ProjectDto>> GetFeaturedProjectsAsync();
    Task<List<ExperienceDto>> GetExperiencesAsync();
    Task<List<SkillCategoryDto>> GetSkillsGroupedAsync();
    Task<List<TestimonialDto>> GetTestimonialsAsync();
    Task<List<SocialDto>> GetSocialsAsync();
    Task<SettingsDto?> GetSettingsAsync();
    Task SubmitContactAsync(ContactFormDto form);
}
