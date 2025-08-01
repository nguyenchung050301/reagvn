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
      
    }
}
