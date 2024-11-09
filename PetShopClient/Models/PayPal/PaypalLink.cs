using System.Text.Json.Serialization;

namespace PetShopClient.Models.PayPal
{
    public class PaypalLink
    {
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("rel")]
        public string? Rel { get; set; }
    }
}
