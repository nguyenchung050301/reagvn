using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Cart
{
    public class CartAddProductDTO
    {
        [Required]
        public string ProductId { get; set; }


     /*   [Required]
        public string ProductType { get; set; } 

        [Required]
        public float UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }*/
    }
}
