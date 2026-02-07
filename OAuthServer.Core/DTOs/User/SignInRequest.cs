namespace OAuthServer.Core.DTOs.User;

public record SignInRequest(

    string Email, 
    string Password);