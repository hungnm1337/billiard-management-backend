using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class OrderTable
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int UserId { get; set; }

    public DateTime Time { get; set; }

    public virtual Table Table { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
