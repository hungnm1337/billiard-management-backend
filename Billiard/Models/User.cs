using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Billiard.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public int AccountId { get; set; }

    public int RoleId { get; set; }

    public DateOnly Dob { get; set; }

    [JsonIgnore]
    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Invoice> InvoiceEmployees { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceUsers { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();

    public virtual ICollection<RewardPoint> RewardPoints { get; set; } = new List<RewardPoint>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<ShiftAssignment> ShiftAssignments { get; set; } = new List<ShiftAssignment>();
}
