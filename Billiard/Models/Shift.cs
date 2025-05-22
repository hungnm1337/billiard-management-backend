using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string ShiftName { get; set; } = null!;

    public TimeOnly Start { get; set; }

    public TimeOnly End { get; set; }

    public virtual ICollection<ShiftAssignment> ShiftAssignments { get; set; } = new List<ShiftAssignment>();
}
