namespace e_commercial.Services.InterfaceService
{
    public interface IGenericCartProductService
    {
        public string ServiceType { get; set; }
        void AddProductToCart(Guid id);
    }
}
