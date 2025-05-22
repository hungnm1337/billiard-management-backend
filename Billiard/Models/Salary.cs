using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double Salary1 { get; set; }

    public virtual User User { get; set; } = null!;
}
