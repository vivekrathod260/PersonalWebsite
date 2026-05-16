using Google.Cloud.Firestore;

using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Social
{
    public string Id { get; set; } = string.Empty;

    [FirestoreProperty("platform")]
    public string Platform { get; set; } = string.Empty;

    [FirestoreProperty("url")]
    public string Url { get; set; } = string.Empty;

    [FirestoreProperty("icon")]
    public string Icon { get; set; } = string.Empty;

    [FirestoreProperty("order")]
    public int Order { get; set; }

    [FirestoreProperty("visible")]
    public bool Visible { get; set; } = true;
}
