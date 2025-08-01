namespace e_commercial.Services.InterfaceService
{
    public interface IProductServiceFactory
    {
        IProductService GetService(string productType);
    }
}
