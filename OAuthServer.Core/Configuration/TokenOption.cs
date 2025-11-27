namespace OAuthServer.Core.Configuration;

public class TokenOption
{
    public List<String> Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; } = default!;
}