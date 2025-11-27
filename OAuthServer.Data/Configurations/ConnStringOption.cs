namespace OAuthServer.Data.Configurations;

public class ConnStringOption
{
    public const string Key = "ConnectionStrings";
    public string SqlServer { get; set; } = default!;
}