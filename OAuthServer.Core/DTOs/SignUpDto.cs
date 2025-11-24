using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.DTOs
{
    // KULLANICIDAN SIGN UP SIRASINDA ALINAN DTO
    public class SignUpDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int NativeLanguageId { get; set; }
    }
}
