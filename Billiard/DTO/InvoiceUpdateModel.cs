namespace Billiard.DTO
{
    public class InvoiceUpdateModel
    {
        public int InvoiceId { get; set; }

        public DateTime? TimeEnd { get; set; }

        public decimal TotalAmount { get; set; }

        public int? UserId { get; set; }

        public string PaymentStatus { get; set; }

    }
}
