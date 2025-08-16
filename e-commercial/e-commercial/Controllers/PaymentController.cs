using e_commercial.Constants;
using e_commercial.DTOs.Request.Payment;
using e_commercial.Exceptions;
using e_commercial.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly JWTService _jwtService;
        public PaymentController(PaymentService paymentService, JWTService jwtService)
        {
            _paymentService = paymentService;
            _jwtService = jwtService;
        }

        [Authorize(Roles = RoleEnum.User)]
        [HttpPost("user/{orderId}/pay")]
        public IActionResult PayOrder([FromBody] PaymentCreateDTO paymentDTO, Guid orderId)
        {
            try
            {
                string userId = _jwtService.ExtractID(User);
                _paymentService.PayOrder(paymentDTO, orderId, Guid.Parse(userId));
                return Ok("Payment successful.");
            }
            catch (BadValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
