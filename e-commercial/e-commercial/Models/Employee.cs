using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string? EmployeeName { get; set; }

    public string? EmployeeGender { get; set; }

    public string? BranchId { get; set; }

    public string? DepartmentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Department? Department { get; set; }
}
