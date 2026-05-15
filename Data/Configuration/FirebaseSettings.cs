namespace Data.Configuration;

public class FirebaseSettings
{
    public const string SectionName = "Firebase";
    public string ProjectId { get; set; } = string.Empty;
    public string FirebaseCredJsonEnvVarName { get; set; } = "FIREBASE_CREDENTIALS_JSON";
}
