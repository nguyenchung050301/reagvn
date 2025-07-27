using Microsoft.AspNetCore.Mvc;

namespace e_commercial.DTOs.Response.RefreshToken
{
    public class RefreshTokenDetailDTO
    {
        public string? TokenValue { get; set; }

        public DateTime? TokenExpires { get; set; }

        public bool? TokenIsrevoked { get; set; }

        public bool? TokenIsused { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
