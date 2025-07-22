using e_commercial.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace e_commercial.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly SigningCredentials creds;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly RsaSecurityKey _privateKey;
        private readonly RSA _rsa;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
           // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            
            tokenHandler = new JwtSecurityTokenHandler();

            //Load private key trong file
            var privateKeyPath = _configuration["JWT:PrivateKeyPath"];
            _rsa = RSA.Create();
            _rsa.ImportFromPem(File.ReadAllText(privateKeyPath).ToCharArray()); //doc file vaf chuyen thanh char array
            _privateKey = new RsaSecurityKey(_rsa); //tao RsaSecurityKey tu RSA

            creds = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256);
        }
        public string GenerateToken(string userName, string userRole)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName), //nhúng data vào token với kiểu claimtype
                new Claim(ClaimTypes.Role, userRole)
            };

           
            //Jwt: Key là một chuỗi bí mật dùng để ký token(nằm trong appsettings.json).
            //SymmetricSecurityKey: tạo ra khóa đối xứng từ chuỗi bí mật.
            //SigningCredentials: định nghĩa thuật toán ký(ở đây là HmacSha256).

            var token = new JwtSecurityToken(
           //     issuer: _configuration["Jwt:Issuer"], //địa chỉ phát hành token
           //     audience: _configuration["Jwt:Audience"], //đối tượng sử dụng token
                claims: claims, //các claim đã định nghĩa ở trên
                expires: DateTime.Now.AddMinutes(30), //thời gian hết hạn của token, convert qua phút
                signingCredentials: creds //các thông tin ký token
                );
            
            if (token == null)
            {
                throw new BadValidationException("Token generation failed", nameof(userName));
            }
            return tokenHandler.WriteToken(token); //trả về token đã được mã hóa


        }
    }
}
