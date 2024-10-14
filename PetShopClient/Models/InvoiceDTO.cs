namespace PetShopClient.Models
{
    public class InvoiceDTO
    {
        public Guid InvoiceId { get; set; }
        public string? InvoiceCode { get; set; }
        public decimal InvoiceAmount { get; set; }
        public decimal? Discount { get; set; }
        public DateTime TimeCharge { get; set; }
        public decimal AmountCharge { get; set; }
        public string? Notes { get; set; }
    }
}
