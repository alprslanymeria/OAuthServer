using System;
using System.Collections.Generic;
using System.Text;

namespace OAuthServer.Core.DTOs
{
    public class RefreshTokenDto
    {
        public string Token { get; set; } = null!;
    }
}
