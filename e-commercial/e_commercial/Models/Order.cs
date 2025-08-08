using System;
using System.Collections.Generic;

namespace e_commercial.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public int? TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UserId { get; set; }
        // Navigation properties
        // public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        // public virtual User? User { get; set; }
    }
}