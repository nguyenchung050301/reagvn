using System.ComponentModel.DataAnnotations;

namespace e_commercial.DTOs.Request.Keyboard
{
    public class KeyboardUpdateDTO
    {

        [StringLength(255)]
        public string? KeyboardName { get; set; }

 
        [StringLength(255)]
        public string? KeyboardSwitch { get; set; }

        [StringLength(255)]
        public string? KeyboardDescription { get; set; }

        public string? KeyboardImage { get; set; }

        public string? CategoryId { get; set; }

        public string? ManufacturerId { get; set; }
    }
}
