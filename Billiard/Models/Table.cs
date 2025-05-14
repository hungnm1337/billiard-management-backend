using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Table
{
    public int TableId { get; set; }

    public string TableName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal HourlyRate { get; set; }

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
