namespace PetShopClient.Models
{
    public class PaymentRequestDTO
    {
        public decimal Amount { get; set; }
        public string? RecipientName { get; set; }
        public string? Description { get; set; }
    }
}
