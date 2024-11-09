using System.Text.Json.Serialization;

namespace PetShopClient.Models.PayPal
{
    public class PaypalPaymentResponse
    {
        [JsonPropertyName("links")]
        public List<PaypalLink>? Links { get; set; }
    }
}
