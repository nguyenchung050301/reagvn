using e_commercial.DTOs.Request.RefreshToken;
using e_commercial.DTOs.Request.User;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace e_commercial.Services
{
    public class RefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private Refreshtoken _refreshToken;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create(); // Sử dụng RandomNumberGenerator để tạo số ngẫu nhiên, using là từ khóa
                                                            //để đảm bảo giải phóng tài nguyên sau khi sử dụng.
            rng.GetBytes(randomBytes); //đổ số ngẫu nhiên vào randomBytes.
            return Convert.ToBase64String(randomBytes); 
        }
        public void CreateToken(RefreshTokenDTO refreshtokenDTO)
        {
            _refreshToken = new Refreshtoken
            {
                TokenId = Guid.NewGuid().ToString(),
                TokenValue = BCrypt.Net.BCrypt.HashPassword(GenerateRefreshToken()),
                TokenExpires = DateTime.UtcNow.AddMinutes(1),
                TokenIsrevoked = false,
                TokenIsused = false,
                CreatedAt = DateTime.UtcNow,
                UserId = refreshtokenDTO.UserId
            };
            _refreshTokenRepository.Add(_refreshToken);
        }
        public void UpdateToken(RefreshTokenDTO refreshtokenDTO, Guid id)
        {
            if (refreshtokenDTO == null)
            {
                throw new BadValidationException("Refresh token is not created.", nameof(refreshtokenDTO));
            }
            var existing = _refreshTokenRepository.GetByID(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Refresh token with ID {id} not found.");
            }
            existing.TokenValue = BCrypt.Net.BCrypt.HashPassword(refreshtokenDTO.TokenValue);
            existing.TokenExpires = DateTime.UtcNow.AddMinutes(1);
            existing.TokenIsrevoked = refreshtokenDTO.TokenIsRevoked;
            existing.TokenIsused = refreshtokenDTO.TokenIsUsed;
            existing.CreatedAt = DateTime.UtcNow;
            existing.UserId = refreshtokenDTO.UserId;
            _refreshTokenRepository.Update(existing);
        }
     
      /*  public RefreshTokenDTO Validate(string tokenValue)
        {
            if (_refreshToken == null)
            {
                throw new BadValidationException("Refresh token is not created.", nameof(_refreshToken));
            }      
            return  _refreshTokenRepository.GetByID(Guid.Parse(_refreshToken.TokenId));
        }*/
      /*  public void GenerateNewRefreshToken(RefreshTokenDTO tokenDTO, Guid id)
        {
            // Đánh dấu token cũ đã dùng
            tokenDTO.TokenIsRevoked = true;
            tokenDTO.TokenIsUsed = true;

            UpdateToken(tokenDTO, id);
            *//*var storedToken = Validate(tokenDTO.TokenValue);
            if (storedToken == null)
                throw new BadValidationException("Invalid refresh token", nameof(tokenDTO.TokenValue));
            *//*

            UserLoginDTO user = new UserLoginDTO
            {
                Username = tokenDTO,
                Userpassword = storedToken.User.Userpassword
            };
            // Tạo access token và refresh token mới
            var accessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _refreshTokenService.CreateToken(storedToken);
        }*/
    }
}
