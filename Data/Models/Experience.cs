using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Experience
{
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty("company")]
    public string Company { get; set; } = string.Empty;

    [FirestoreProperty("role")]
    public string Role { get; set; } = string.Empty;

    [FirestoreProperty("description")]
    public string Description { get; set; } = string.Empty;

    [FirestoreProperty("startDate")]
    public string StartDate { get; set; } = string.Empty;

    [FirestoreProperty("endDate")]
    public string EndDate { get; set; } = string.Empty;

    [FirestoreProperty("current")]
    public bool Current { get; set; }

    [FirestoreProperty("order")]
    public int Order { get; set; }
}
