using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public decimal? Salary { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<RewardPoint> RewardPoints { get; set; } = new List<RewardPoint>();

    public virtual Role Role { get; set; } = null!;
}
