using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class ContactMessage
{
    [FirestoreProperty("name")]
    public string Name { get; set; } = string.Empty;

    [FirestoreProperty("email")]
    public string Email { get; set; } = string.Empty;

    [FirestoreProperty("subject")]
    public string Subject { get; set; } = string.Empty;

    [FirestoreProperty("message")]
    public string Message { get; set; } = string.Empty;

    [FirestoreProperty("createdAt")]
    public Timestamp CreatedAt { get; set; }
}
