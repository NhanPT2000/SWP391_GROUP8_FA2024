using System.Net.Http.Headers;
using System.Text;
using DataObject;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using PetShopClient.Models.PayPal;
using PetShopClient.Util.Interface;

namespace PetShopClient.Util.Implement
{
    public class PayPalService : IPayPalService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private DateTime _tokenExpiry;

        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration["Paypal:sandboxUrl"]) };
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_accessToken != null && DateTime.UtcNow < _tokenExpiry)
                return _accessToken;

            var clientId = _configuration["Paypal:Key"];
            var secret = _configuration["Paypal:Secret"];
            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{secret}"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            })
            };

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to retrieve PayPal access token.");

            var tokenData = await response.Content.ReadAsAsync<dynamic>();
            _accessToken = tokenData.access_token;
            _tokenExpiry = DateTime.UtcNow.AddSeconds((int)tokenData.expires_in - 300);

            return _accessToken;
        }

        public async Task<string> CreatePaymentAsync(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var paymentData = new
            {
                intent = "sale",
                redirect_urls = new { return_url = returnUrl, cancel_url = cancelUrl },
                payer = new { payment_method = "paypal" },
                transactions = new[]
                {
            new {
                amount = new { total = amount.ToString("F2"), currency = currency }
            }
        }
            };

            var response = await _httpClient.PostAsJsonAsync("/v1/payments/payment", paymentData);
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to create PayPal payment.");

            var result = await response.Content.ReadAsAsync<dynamic>();

            var approvalLink = ((IEnumerable<dynamic>)result.links)
                .FirstOrDefault(link => link.rel == "approval_url")?.href;

            if (approvalLink == null)
            {
                throw new Exception("Approval link not found in PayPal response.");
            }

            return approvalLink;
        }

        public async Task<bool> CapturePaymentAsync(string paymentId)
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.PostAsync($"/v1/payments/payment/{paymentId}/execute", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExecutePaymentAsync(string paymentId, string payerId)
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var executeData = new { payer_id = payerId };

            var response = await _httpClient.PostAsJsonAsync($"/v1/payments/payment/{paymentId}/execute", executeData);

            return response.IsSuccessStatusCode;
        }
    }
}
