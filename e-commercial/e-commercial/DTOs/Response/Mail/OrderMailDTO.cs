using e_commercial.Constants;
using e_commercial.Models;

namespace e_commercial.DTOs.Response.Mail
{
    public class OrderMailDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
        public List<(string, ProductTypeEnum, float, int)> ProductDetail { get; set; } //Tuple: Name, Type, Price, Quantity
    //    public List<Orderdetail> Orderdetails { get; set; }    
    }
}
