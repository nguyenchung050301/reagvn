using System;
using System.Collections.Generic;

namespace e_commercial.Models.Products;

public partial class Keyboard
{
    public string KeyboardId { get; set; } = null!;

    public string? KeyboardName { get; set; }

    public string? KeyboardSwitch { get; set; }

    public string? KeyboardDescription { get; set; }

    public string? KeyboardImage { get; set; }

    public string CategoryId { get; set; } = null!;

    public string ManufacturerId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public int? StockQuantity { get; set; }

    public float? Price { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;
}
