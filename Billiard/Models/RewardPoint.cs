using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class RewardPoint
{
    public int RewardPointsId { get; set; }

    public int UserId { get; set; }

    public double Points { get; set; }

    public virtual User User { get; set; } = null!;
}
