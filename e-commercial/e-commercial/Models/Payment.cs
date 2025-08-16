using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public float? PaymentPrice { get; set; }

    public string? PaymentType { get; set; }

    public string? OrderId { get; set; }

    public string? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? User { get; set; }
}
