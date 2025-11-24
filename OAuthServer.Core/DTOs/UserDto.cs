using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.DTOs
{
    // KULLANICIYA SIGN UP İŞLEMİNDEN SONRA DÖNÜLEN DTO
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string Email { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int NativeLanguageId { get; set; }
    }
}
