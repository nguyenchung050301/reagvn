using e_commercial.DTOs.Request.Order;
using e_commercial.Services;
using e_commercial.Services.ServiceFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commercial.Controllers
{
  /*  [Authorize]
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

      
    }*/
}
