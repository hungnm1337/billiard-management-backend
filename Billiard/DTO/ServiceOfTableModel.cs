namespace Billiard.DTO
{
    public class ServiceOfTableModel
    {
        public int TableId { get; set; }
        public int InvoiceId { get; set; }
        public List<ServiceItemDto> Services { get; set; } = new List<ServiceItemDto>();
    }

    public class ServiceItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
