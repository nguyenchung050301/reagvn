namespace e_commercial.Services.InterfaceService
{
    public interface IProductService
    {

        public void AddProductToCart(Guid id);
        public void DecreaseStock(Guid id, int quantity);

    }
}
