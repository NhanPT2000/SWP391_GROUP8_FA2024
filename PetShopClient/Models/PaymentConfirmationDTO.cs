namespace PetShopClient.Models
{
    public class PaymentConfirmationDTO
    {
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
