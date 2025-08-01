using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Orderdetail
{
    public string OrderDetailId { get; set; } = null!;

    public int Quantity { get; set; }

    public float UnitPrice { get; set; }

    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
