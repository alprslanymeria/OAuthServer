using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.DTOs
{
    // KULLANICIDAN SIGN IN SIRASINDA ALINAN DTO
    public class SignInDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    //public class ClientSignInDto
    //{
    //    public string ClientId { get; set; } = null!;
    //    public string ClientSecret { get; set; } = null!;
    //}
}
