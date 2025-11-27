namespace OAuthServer.Core.Models;

public class UserRefreshToken
{
    public string UserId { get; set; } = default!;
    public string Code { get; set; } = default!;
    public DateTime Expiration { get; set; }
}
