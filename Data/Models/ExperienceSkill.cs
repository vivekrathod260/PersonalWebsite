using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class ExperienceSkill
{
    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty("iconUrl")]
    public string IconUrl { get; set; } = string.Empty;

    [FirestoreProperty("highlight")]
    public bool Highlight { get; set; } = false;
}
