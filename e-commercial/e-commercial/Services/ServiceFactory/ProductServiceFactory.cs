

using e_commercial.DTOs.Response.Order;

namespace e_commercial.Services.ServiceFactory
{
    public class ProductServiceFactory
    {
        private Dictionary<string, IQueryable> _serviceDicts = new Dictionary<string, IQueryable>();
        
        public ProductServiceFactory(string id)
        {
         //   _serviceDicts.Add("Keyboard", Select id f);
         //   _serviceDicts.Add("Laptop", );
          
        }

    }
}
