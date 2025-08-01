using e_commercial.Models.Products;
using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Manufacturer
{
    public string ManufacturerId { get; set; } = null!;

    public string? ManufacturerName { get; set; }

    public string? ManufacturerAddress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Keyboard> Keyboards { get; set; } = new List<Keyboard>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}
