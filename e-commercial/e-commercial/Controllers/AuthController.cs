using e_commercial.Constants;
using e_commercial.DTOs.Request.RefreshToken;
using e_commercial.DTOs.Request.User;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly RefreshTokenService _refreshTokenService;
        private readonly JWTService _jwtService;
        private readonly UserService _userService;
        public AuthController(RefreshTokenService refreshTokenService, JWTService jWTService, UserService userService)
        {
            _refreshTokenService = refreshTokenService;
            _jwtService = jWTService;
            _userService = userService;
        }

        [HttpPost("refresh-token/{id}")]
        public IActionResult RefreshToken([FromBody]RefreshTokenDTO tokenDTO, Guid id)
        {



            return Ok(tokenDTO);
        }
        /// <summary>
        /// dang nhap: nhan object co 2 field username va password
        /// tim kiem username trong database trong table users, neu ko tim thay tra badvalidation voi message "username hoawc pass sai" (moi case)
        /// Neu tim thay thi kiem tra password bang cach su dung bcrypt.verify
        /// Neu verify = false tra ve message badvalidation
        /// Neu login thanh cong, tra ve JWT token vaf mot so thong tin di kem
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO userDTO)
        {
            try
            {           
                User user = _userService.LoadByUserName(userDTO);
                var token = _jwtService.GenerateToken(user);
                return Ok(new
                {
                    token = token,
                    message = "Login successful",
                    username = user.Username,
                    role = user.UserRole,
                });
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
