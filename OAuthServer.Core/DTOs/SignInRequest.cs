namespace OAuthServer.Core.DTOs;

// KULLANICIDAN SIGN IN SIRASINDA ALINAN DTO
public record SignInRequest (string Email, string Password);


//public class ClientSignInDto
//{
//    public string ClientId { get; set; } = null!;
//    public string ClientSecret { get; set; } = null!;
//}