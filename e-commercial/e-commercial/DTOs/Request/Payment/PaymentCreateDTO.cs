using e_commercial.Constants;
using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Payment
{
    public class PaymentCreateDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "OrderId must be greater than 0.")]
        public float PaymentPrice { get; set; }


        [Required]
        public PaymentEnum PaymentType { get; set; }
    }   
}
