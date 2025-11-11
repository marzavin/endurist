namespace Endurist.Hosting.Settings;

/// <summary>
/// Authentication configuration.
/// </summary>
public class AuthenticationConfiguration
{
    public string Secret { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public int ExpiresIn { get; set; }
}