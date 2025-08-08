using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Laptop
{
    public string LaptopId { get; set; } = null!;

    public string? LaptopName { get; set; }

    public float? LaptopSize { get; set; }

    public string? LaptopDescription { get; set; }

    public string? LaptopImage { get; set; }

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
