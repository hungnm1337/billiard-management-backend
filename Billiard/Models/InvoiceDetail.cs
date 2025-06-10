using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class InvoiceDetail
{
    public int ServiceTableId { get; set; }

    public int InvoiceId { get; set; }

    public int TableId { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; }

    public virtual Service Service { get; set; } = null!;
}
