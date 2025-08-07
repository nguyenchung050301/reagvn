using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Userpassword { get; set; }

    public string? UserRole { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public string? UserShownname { get; set; }

    public string? UserDistrict { get; set; }

    public string? UserWard { get; set; }

    public string? UserAddress { get; set; }

    public string? UserPhone { get; set; }

    public string? UserEmail { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
