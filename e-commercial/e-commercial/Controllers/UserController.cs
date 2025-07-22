using e_commercial.Constants;
using e_commercial.DTOs.Request.User;
using e_commercial.Exceptions;
using e_commercial.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JWTService _jwtService;
        public UserController(UserService userService, JWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// dang ky: ten tai khoan co the nhap sdt hoac email
        /// + username ko dc trung`, password luu database phai ma hoa (bcrypt, md5)
        /// field bat buoc: username, password, ten khach hang, dia chi, sdt, user role default la user
        ///
        [HttpPost("register")]
        public IActionResult Register(UserCreateDTO user)
        {
            try
            {
                _userService.Register(user);
                return Created();
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);

            }
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
                var token = _jwtService.GenerateToken(userDTO.Username, RoleEnum.User);
                _userService.Login(userDTO);
                return Ok(new
                {
                    token = token,
                    message = "Login successful",
                    username = userDTO.Username,
                    role = RoleEnum.User,
                });
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
