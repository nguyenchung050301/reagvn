using e_commercial.Services;
using e_commercial.Services.ServiceFactory;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly CartService _cartService;
        private readonly CartProductServiceFactory _cartProductServiceFactory;
        public CartController(CartService cartService, CartProductServiceFactory cartProductServiceFactory)
        {
            _cartService = cartService;
            _cartProductServiceFactory = cartProductServiceFactory;
        }
        [HttpGet]
        public IActionResult ShowProducts()
        {
            return Ok(_cartService.ShowAllCarts());
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart([FromBody] Guid id, [FromBody] string type)
        {
            try
            {
                
                _cartProductServiceFactory.GetService(type).AddProductToCart(id); //type dang sai
                return Created("", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
