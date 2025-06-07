namespace Billiard.DTO
{
    public class ShiftModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ShiftId { get; set; }

        public DateOnly Day { get; set; }

        public string Status { get; set; } = null!;
    }
}
