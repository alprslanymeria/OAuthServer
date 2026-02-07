using Microsoft.AspNetCore.Identity;

namespace OAuthServer.Core.Models;

public class User : IdentityUser
{
    public string? Image { get; set; }
    public int NativeLanguageId { get; set; }
}
