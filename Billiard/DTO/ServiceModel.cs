namespace Billiard.DTO
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
