namespace Billiard.DTO
{
    public class BookingTableModel
    {
        public int TableId { get; set; }

        public int UserId { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
    }
}
