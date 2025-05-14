using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
