using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class ShiftAssignment
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int ShiftId { get; set; }

    public DateOnly Day { get; set; }

    public string Status { get; set; } = null!;

    public virtual User Employee { get; set; } = null!;

    public virtual Shift Shift { get; set; } = null!;
}
