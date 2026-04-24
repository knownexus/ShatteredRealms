namespace ShatteredRealms.Application.Settings;

public sealed class ConfirmationSettings
{
    public const string SectionName = "ConfirmationSettings";

    /// <summary>
    /// When true, users must click an emailed confirmation link before they can sign in.
    /// When false, email is auto-confirmed on registration and sign-in is immediate.
    /// Can be changed in appsettings.json while the application is running.
    /// </summary>
    public bool RequireEmailConfirmation { get; set; } = true;

    /// <summary>
    /// Reserved for a future phone-verification flow.
    /// Can be changed in appsettings.json while the application is running.
    /// </summary>
    public bool RequirePhoneConfirmation { get; set; } = false;
}
