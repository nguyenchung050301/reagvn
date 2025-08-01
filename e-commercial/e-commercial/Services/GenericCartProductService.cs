using e_commercial.Data;
using e_commercial.Exceptions;
using e_commercial.Models;
using e_commercial.Services.InterfaceService;

namespace e_commercial.Services
{
    public class GenericCartProductService<T> : IGenericCartProductService where T : class
    {
        private readonly ReagvnContext _context;
        private readonly CartService _cartService;
        private readonly Func<T, (string id, string productType,float price, int quantity)> _extractor;
        //Func<Input, Output> là một hàm nhận đầu vào là Input và trả về Output

        public string ServiceType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GenericCartProductService( ReagvnContext context, CartService cartService,
            Func<T, (string id, string productType,float price, int quantity)> extractor, string serviceType)
        {
            _context = context;
            _cartService = cartService;
            _extractor = extractor;
            ServiceType = serviceType;
        }

       

        public void AddProductToCart(Guid id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new BadValidationException(ServiceType + " with ID " + id + " not found.", nameof(entity));
            }

            var (productId, productType, price, quantity) = _extractor(entity); //gán giá trị trả về từ hàm extractor vào các biến

            _cartService.AddToCart(new Cart
            {
                CartId = Guid.NewGuid().ToString(),
                ProductId = productId,
                ProductType = productType,
                UnitPrice = price,
                Quantity = 1
            });
        }
        public void GetType()
        {

        }

    }
}
