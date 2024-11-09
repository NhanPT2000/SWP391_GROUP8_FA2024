using DataObject;
using PetShopClient.Models.PayPal;

namespace PetShopClient.Util.Interface
{
    public interface IPayPalService
    {
        Task<string> GetAccessTokenAsync();
        Task<string> CreatePaymentAsync(decimal amount, string currency, string returnUrl, string cancelUrl);
        Task<bool> CapturePaymentAsync(string paymentId);
        Task<bool> ExecutePaymentAsync(string paymentId, string payerId);
    }
}
