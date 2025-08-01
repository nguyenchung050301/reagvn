using e_commercial.Exceptions;
using e_commercial.Services.InterfaceService;
using Microsoft.IdentityModel.Tokens;

namespace e_commercial.Services.ServiceFactory
{
    public class CartProductServiceFactory
    {

        private readonly Dictionary<string, IGenericCartProductService> _serviceDicts;
        public CartProductServiceFactory(IEnumerable<IGenericCartProductService> services)
        {
            _serviceDicts = services.ToDictionary(
                  s => s.ServiceType.ToLower(), // Assuming ProductType is a property of the service
                    s => s
            );
        }
        public IGenericCartProductService GetService(string serviceType)
        {
            if (_serviceDicts.IsNullOrEmpty())
            {
                throw new BadValidationException("No services registered for product type: " + serviceType,
                    nameof(serviceType));
            }

            if (!_serviceDicts.TryGetValue(serviceType.ToLower(), out var service))
            {
                throw new BadValidationException("No service found for product type: " + serviceType,
                    nameof(serviceType));
            }

            return service; //service from _serviceDicts.TryGetValue(productType.ToLower(), out var service)
        }

    }
}
