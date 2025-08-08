using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Response.Cart
{
    public class CartGetProductDTO
    {
        [Required]
        public string ProductId { get; set; }


        public string ProductType { get; set; }

        public float UnitPrice { get; set; }

        public int Quantity { get; set; }

    }
}
