
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Repositories.Interfaces;

namespace e_commercial.Services
{
    public class CartService 
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void AddToCart(Cart cart)
        {
            if (cart == null)
            {
                throw new BadValidationException("Cart cannot be null");
            }
            var existingCart = _cartRepository.GetByID(Guid.Parse(cart.ProductId));
            if (existingCart != null)
            {
                existingCart.Quantity += 1; // Increase quantity if product already exists in cart
            }
            _cartRepository.Add(cart);
        }
        public IEnumerable<Cart> ShowAllCarts()
        {
            var carts = _cartRepository.GetAll();
            return carts.Select(p => new Cart
            {
                ProductId = p.ProductId,
                ProductType = p.ProductType,
                UnitPrice = p.UnitPrice,
                Quantity = p.Quantity
            });

        }
    }
}
