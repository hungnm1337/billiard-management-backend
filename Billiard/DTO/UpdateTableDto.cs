namespace Billiard.DTO
{
    public class UpdateTableDto
    {
        public int TableId { get; set; }

        public string TableName { get; set; } = null!;

        public decimal HourlyRate { get; set; }
    }
}
