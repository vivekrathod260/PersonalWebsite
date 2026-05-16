using Google.Cloud.Firestore;

using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Project
{
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty("title")]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty("description")]
    public string Description { get; set; } = string.Empty;

    [FirestoreProperty("techStack")]
    public List<string> TechStack { get; set; } = [];

    [FirestoreProperty("images")]
    public List<string> Images { get; set; } = [];

    [FirestoreProperty("videoUrl")]
    public string VideoUrl { get; set; } = string.Empty;

    [FirestoreProperty("githubUrl")]
    public string GithubUrl { get; set; } = string.Empty;

    [FirestoreProperty("liveUrl")]
    public string LiveUrl { get; set; } = string.Empty;

    [FirestoreProperty("tags")]
    public List<string> Tags { get; set; } = [];

    [FirestoreProperty("featured")]
    public bool Featured { get; set; }

    [FirestoreProperty("order")]
    public int Order { get; set; }

    [FirestoreProperty("visible")]
    public bool Visible { get; set; } = true;
}
