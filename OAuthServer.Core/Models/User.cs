using Microsoft.AspNetCore.Identity;

namespace OAuthServer.Core.Models;

public class User : IdentityUser
{
    public string? ImageUrl { get; set; }
}
