using e_commercial.Constants;

namespace e_commercial.DTOs.Response.Order
{
    public class OrderItemDTO
    {
        public string Id { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
