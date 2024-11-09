using System.Text.Json.Serialization;

namespace PetShopClient.Models.PayPal
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }
}
