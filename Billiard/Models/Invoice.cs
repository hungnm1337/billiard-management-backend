using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public DateTime TimeStart { get; set; }

    public DateTime TimeEnd { get; set; }

    public decimal TotalAmount { get; set; }

    public int EmployeeId { get; set; }

    public int UserId { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual User Employee { get; set; } = null!;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}
