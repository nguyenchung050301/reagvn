using e_commercial.Services.InterfaceService;

namespace e_commercial.Services.ServiceFactory
{
    public class ProductServiceFactory : IProductServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, IProductService> _serviceDicts = new Dictionary<string, IProductService>();
        public ProductServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            // Register services in the dictionary
            _serviceDicts.Add("keyboard", _serviceProvider.GetRequiredService<KeyboardServicce>());
        }
        public IProductService GetService(string productType)
        {
            return productType.ToLower() switch
            {
                "keyboard" => _serviceDicts["keyboard"],
                //  "laptop" => _serviceProvider.GetRequiredService<LaptopService>(),
                _ => throw new ArgumentException("Invalid product type", nameof(productType))
            };
        }
    }
}
