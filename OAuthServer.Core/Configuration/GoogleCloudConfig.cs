namespace OAuthServer.Core.Configuration;

/// <summary>
/// CONFIGURATION OPTIONS FOR GOOGLE CLOUD TRANSLATE API.
/// </summary>
public class GoogleCloudConfig
{
    public const string Key = "GoogleCloudConfig";

    /// <summary>
    /// PATH TO THE GOOGLE APPLICATION CREDENTIALS JSON FILE.
    /// </summary>
    public string CredentialsPath { get; set; } = null!;
}