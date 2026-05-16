using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Skill
{
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty("category")]
    public string Category { get; set; } = string.Empty;

    [FirestoreProperty("iconUrl")]
    public string IconUrl { get; set; } = string.Empty;

    [FirestoreProperty("proficiency")]
    public int Proficiency { get; set; }

    [FirestoreProperty("order")]
    public int Order { get; set; }

    [FirestoreProperty("visible")]
    public bool Visible { get; set; } = true;
}
