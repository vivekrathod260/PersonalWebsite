using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Profile
{
    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty("title")]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty("intro")]
    public string Intro { get; set; } = string.Empty;

    [FirestoreProperty("about")]
    public string About { get; set; } = string.Empty;

    [FirestoreProperty("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [FirestoreProperty("resumeUrl")]
    public string ResumeUrl { get; set; } = string.Empty;

    [FirestoreProperty("email")]
    public string Email { get; set; } = string.Empty;

    [FirestoreProperty("location")]
    public string Location { get; set; } = string.Empty;

    [FirestoreProperty("ctaPrimary")]
    public string CtaPrimary { get; set; } = string.Empty;

    [FirestoreProperty("ctaSecondary")]
    public string CtaSecondary { get; set; } = string.Empty;
}
