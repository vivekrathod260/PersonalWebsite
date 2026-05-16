using Google.Cloud.Firestore;

namespace Data.Models;

[FirestoreData]
public class Settings
{
    [FirestoreProperty("showHero")]
    public bool ShowHero { get; set; } = true;

    [FirestoreProperty("showAbout")]
    public bool ShowAbout { get; set; } = true;

    [FirestoreProperty("showExperience")]
    public bool ShowExperience { get; set; } = true;

    [FirestoreProperty("showProjects")]
    public bool ShowProjects { get; set; } = true;

    [FirestoreProperty("showSkills")]
    public bool ShowSkills { get; set; } = true;

    [FirestoreProperty("showTestimonials")]
    public bool ShowTestimonials { get; set; } = true;

    [FirestoreProperty("showContact")]
    public bool ShowContact { get; set; } = true;

    [FirestoreProperty("showFooter")]
    public bool ShowFooter { get; set; } = true;

    [FirestoreProperty("showNavbar")]
    public bool ShowNavbar { get; set; } = true;
}
