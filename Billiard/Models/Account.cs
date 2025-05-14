using System;
using System.Collections.Generic;

namespace Billiard.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Avt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
