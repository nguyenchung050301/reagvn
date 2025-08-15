using e_commercial.Constants;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Request.User;
using e_commercial.Exceptions;
using e_commercial.Services;
using e_commercial.Services.ParentService;
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
        private readonly ProductService _productService;
        public UserController(UserService userService, JWTService jwtService, ProductService productService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _productService = productService;
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

        [HttpPost("product/search")]
        public IActionResult SearchProduct([FromBody] ProductPaginationRequestDTO requestDTO)
        {
            var result = _productService.GetPagination(requestDTO);
            return Ok(result); 
        }
    }
}
