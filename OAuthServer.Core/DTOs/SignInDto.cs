namespace OAuthServer.Core.DTOs;

// KULLANICIDAN SIGN IN SIRASINDA ALINAN DTO
public class SignInDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

//public class ClientSignInDto
//{
//    public string ClientId { get; set; } = null!;
//    public string ClientSecret { get; set; } = null!;
//}