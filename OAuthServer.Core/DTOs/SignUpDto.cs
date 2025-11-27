namespace OAuthServer.Core.DTOs;

// KULLANICIDAN SIGN UP SIRASINDA ALINAN DTO
public class SignUpDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int NativeLanguageId { get; set; }
}