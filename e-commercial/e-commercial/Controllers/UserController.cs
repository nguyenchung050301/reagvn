using e_commercial.Constants;
using e_commercial.DTOs.Request.Pagination;
using e_commercial.DTOs.Request.User;
using e_commercial.DTOs.Response.User;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Services;
using e_commercial.Services.ParentService;
using HandlebarsDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;

        private readonly ProductService _productService;
        public UserController(UserService userService, ProductService productService)
        {

            _userService = userService;
            _productService = productService;
        }
        /// <summary>
        /// dang ky: ten tai khoan co the nhap sdt hoac email
        /// + username ko dc trung`, password luu database phai ma hoa (bcrypt, md5)
        /// field bat buoc: username, password, ten khach hang, dia chi, sdt, user role default la user
        ///
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDTO user)
        {
            await _userService.Register(user);
            return Created();
        }

        [HttpPost("verify-otp")]
        public IActionResult VerifyOTP(UserVerificationDTO verificationDTO)
        {
            _userService.VerifyOTP(verificationDTO);
            return Ok();
        }

        [HttpPost("verify-resend")]
        public async Task<IActionResult> SendOtp([FromBody] UserMailDTO mailDTO,string userId)
        {
            await _userService.SendOtpToMail(mailDTO, userId);
            return Ok();
        }
        [HttpPost("product/search")]
        public IActionResult SearchProduct([FromBody] ProductPaginationRequestDTO requestDTO)
        {
            var result = _productService.GetPagination(requestDTO);
            return Ok(result);
        }
    }
}
