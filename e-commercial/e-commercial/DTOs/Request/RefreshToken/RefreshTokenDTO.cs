using Microsoft.AspNetCore.Mvc;

namespace e_commercial.DTOs.Request.RefreshToken
{
    public class RefreshTokenDTO
    {
        public string? TokenValue { get; set; }

        public bool? TokenIsUsed { get; set; }

        public bool? TokenIsRevoked { get; set; }

        public string? UserId { get; set; }
    }
}
