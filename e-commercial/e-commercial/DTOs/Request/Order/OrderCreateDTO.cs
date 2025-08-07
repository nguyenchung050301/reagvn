using e_commercial.Constants;
using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Order
{
    public class OrderCreateDTO //da dang nhap
    {

        public class CartItemDTO
        {
            [Required]
            public string ProductId { get; set; }


            [Required]
            public ProductTypeEnum ProductType { get; set; }

            /*  [Required]
              [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
              public float UnitPrice { get; set; }*/

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
            public int Quantity { get; set; }
        }
        public IEnumerable<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();

        public string? District { get; set; }

        public string? Ward { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

    }


}
