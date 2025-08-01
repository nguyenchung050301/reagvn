using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Cart
{
    public string CartId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public float UnitPrice { get; set; }

    public int Quantity { get; set; }
}
