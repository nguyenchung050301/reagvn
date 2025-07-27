using e_commercial.Constants;
using e_commercial.DTOs.Request.User;
using e_commercial.Exceptions;
using e_commercial.Models;
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
        private readonly SymmetricSecurityKey _privateKey2;
        private readonly RSA _rsa;
        private double dateExpire;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
            string getExpire = _configuration["JWT:DateExpire"];
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            dateExpire = double.Parse(getExpire != null ? getExpire : "1"); //thời gian hết hạn của token, lấy từ appsettings.json
            tokenHandler = new JwtSecurityTokenHandler();

            _privateKey2 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])); //lấy chuỗi bí mật từ appsettings.json và tạo RsaSecurityKey


            /*    //Load private key trong file
                var privateKeyPath = _configuration["JWT:PrivateKeyPath"];
                _rsa = RSA.Create();
                _rsa.ImportFromPem(File.ReadAllText(privateKeyPath).ToCharArray()); //doc file vaf chuyen thanh char array
                _privateKey = new RsaSecurityKey(_rsa); //tao RsaSecurityKey tu RSA*/

            creds = new SigningCredentials(_privateKey2, SecurityAlgorithms.HmacSha256);
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId), //nhúng data vào token với kiểu claimtype
                new Claim(JwtRegisteredClaimNames.Email, user.UserEmail), //cần xem lại chỗ này
            };

           
            //Jwt: Key là một chuỗi bí mật dùng để ký token(nằm trong appsettings.json).
            //SymmetricSecurityKey: tạo ra khóa đối xứng từ chuỗi bí mật.
            //SigningCredentials: định nghĩa thuật toán ký(ở đây là HmacSha256).

            var token = new JwtSecurityToken(
           //     issuer: _configuration["Jwt:Issuer"], //địa chỉ phát hành token
           //     audience: _configuration["Jwt:Audience"], //đối tượng sử dụng token
                claims: claims, //các claim đã định nghĩa ở trên
                expires: DateTime.UtcNow.AddDays(dateExpire), //thời gian hết hạn của token
                signingCredentials: creds, //các thông tin ký token

                );
          
            /*if (token == null)
            {
                throw new BadValidationException("Token generation failed", nameof(userDTO.Username));
            }*/
            return tokenHandler.WriteToken(token); //trả về token đã được mã hóa


        }
        public bool IsTokenExpired(string _token)
        {
            var token = tokenHandler.ReadToken(_token);
            var expiry = token.ValidTo; //Lay thoi gian het han cua token 
            return expiry < DateTime.Now;
        }
        public string GetJwtSecurityToken(string _token)
        {
            var token = (JwtSecurityToken)tokenHandler.ReadToken(_token);
            Console.WriteLine("Token: " + token);
            return token.ToString();
        }
        public JwtSecurityTokenHandler GetJwtSecurityTokenHandler()
        {
            return tokenHandler;
        }
    }
}
