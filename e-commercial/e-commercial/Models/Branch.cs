using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Branch
{
    public string BranchId { get; set; } = null!;

    public string? BranchName { get; set; }

    public string? BranchAddress { get; set; }

    public string? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual User? User { get; set; }
}
