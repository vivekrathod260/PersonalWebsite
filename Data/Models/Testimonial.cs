using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Testimonial
{
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty("company")]
    public string Company { get; set; } = string.Empty;

    [FirestoreProperty("designation")]
    public string Designation { get; set; } = string.Empty;

    [FirestoreProperty("review")]
    public string Review { get; set; } = string.Empty;

    [FirestoreProperty("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [FirestoreProperty("order")]
    public int Order { get; set; }
}
