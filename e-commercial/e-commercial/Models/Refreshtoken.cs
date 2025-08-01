using System;
using System.Collections.Generic;

namespace e_commercial.Models;

public partial class Refreshtoken
{
    public string? TokenId { get; set; }

    public string? TokenValue { get; set; }

    public DateTime? TokenExpires { get; set; }

    public bool? TokenIsrevoked { get; set; }

    public bool? TokenIsused { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }
}
