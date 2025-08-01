using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public int? TotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual User? User { get; set; }
}
