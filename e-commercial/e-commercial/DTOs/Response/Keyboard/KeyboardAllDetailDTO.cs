namespace e_commercial.DTOs.Response.Keyboard
{
    public class KeyboardAllDetailDTO
    {
        public string KeyboardId { get; set; } = null!;

        public string? KeyboardName { get; set; }

        public string? KeyboardSwitch { get; set; }

        public string? KeyboardDescription { get; set; }

        public string? KeyboardImage { get; set; }
        
        public string? CategoryName { get; set; }   

        public string? ManufacturerName { get; set; }
    }
}
