using DataAccess.Repository;
using DataAccess.Service;
using DataObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using NuGet.Protocol;
using PetShopClient.Helper;
using PetShopClient.Util.Implement;
using PetShopClient.Util.Interface;
using System.Configuration;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMemberService _memberService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IPayPalService _payPalService;
        private readonly IInvoiceService _invoiceService;

        public CartController(
            IOrderService orderService,
            IOrderDetailsService orderDetailsService,
            IMemberService memberService,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IPayPalService payPalService,
            IInvoiceService invoiceService)
        {
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
            _memberService = memberService;
            _contextAccessor = httpContextAccessor;
            _configuration = configuration;
            _payPalService = payPalService;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {

                return RedirectToAction("Login", "Access", new { returnUrl = Url.Action("Index") });
            }
            var orders = await _orderService.GetOrdersAsync(Guid.Parse(userIdClaim));
            var user = await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
            if (user != null)
            {
                ViewBag.UserId = userIdClaim;
                ViewBag.UserName = user.UserName;
            }
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {

                return RedirectToAction("Login", "Access", new { returnUrl = Url.Action("Index") });
            }

            var newOrder = new DataObject.Order
            {
                OrderId = Guid.NewGuid(),
                MemberId = Guid.Parse(userIdClaim),
                OrderDate = DateTime.Now,
                Freight = 0,
                RequiredDate = DateTime.Now.AddDays(7),
                ShippedDate = null
            };
            await _orderService.CreateOrderAsync(newOrder);

            var orderDetails = new OrderDetails
            {
                OrderId = newOrder.OrderId,
                ProductId = productId,
                Quantity = quantity,
                Discount = 0,
                UnitPrice = 0
            };

            await _orderDetailsService.CreateOrderDetailsAsync(orderDetails);

            return RedirectToAction("Index", "Cart");
        }
        [HttpGet("Cart/Cancel/{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var getdeleted = await _orderService.DeleteOrderAsync(id);
            if (getdeleted == false) return View();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Access", new { returnUrl = Url.Action("Checkout") });
            }

            var orders = await _orderService.GetOrdersAsync(Guid.Parse(userIdClaim));
            var amount = 10000.00M; //fixed shipping amount
            foreach (var order in orders)
            {
                foreach(var od in order.OrderDetails)
                {
                    amount += ((decimal)od.UnitPrice * (decimal)od.Quantity);
                }
            }

            var currency = "USD";

            try
            {
                var returnUrl = Url.Action("PaymentSuccess", "Cart", null, Request.Scheme);
                var cancelUrl = Url.Action("PaymentCancelled", "Cart", null, Request.Scheme);

                var approvalLink = await _payPalService.CreatePaymentAsync(amount, currency, returnUrl, cancelUrl);
                return Redirect(approvalLink);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error during checkout: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PaymentSuccess(string paymentId, string token, string PayerID)
        {
            if (string.IsNullOrEmpty(paymentId) || string.IsNullOrEmpty(PayerID))
            {
                ViewData["ErrorMessage"] = "Payment details are missing. Please try again.";
                return View("Error");
            }

            try
            {
                List<Invoice> invoices = new List<Invoice>();

                var paymentResult = await _payPalService.ExecutePaymentAsync(paymentId, PayerID);

                if (paymentResult)
                {
                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    var ordersId = await _orderService.GetOrdersAsync(Guid.Parse(userIdClaim));

                    foreach (var o in ordersId) {

                        if (o == null)
                        {
                            ViewData["ErrorMessage"] = "Order not found.";
                            return View("Error");
                        }
                        var order = await _orderService.GetOrderByIdAsync(o.OrderId);
                        decimal invoiceAmount = (decimal)(o.OrderDetails?.Sum(d => d.UnitPrice * d.Quantity) ?? 0);
                        decimal discount = 0;
                        decimal amountCharge = invoiceAmount - discount;
                        /*order.IsPaid = true;
                        var update = await _orderService.UpdateOrderAsync(order, order.OrderId);*/

                        var invoice = new Invoice
                        {
                            CaseId = null,
                            InvoiceId = Guid.NewGuid(),
                            InvoiceCode = "INV" + DateTime.Now.Ticks.ToString(),
                            OrderId = order.OrderId,
                            TimeGenerated = DateTime.UtcNow.Hour,
                            InvoiceAmount = invoiceAmount,
                            Discount = discount,
                            AmountCharge = amountCharge,
                            TimeCharge = DateTime.UtcNow,
                            Status = "Paid",
                            PaymentId = paymentId,
                            PayerId = PayerID,
                            Notes = "Payment successfully completed via PayPal"
                        };

                        await _invoiceService.CreateInvoiceAsync(invoice);
                        invoices.Add(await _invoiceService.GetInvoiceByIdAsync(invoice.InvoiceId));
                    }
                    var removeOrder = _orderService.PaidOrder(Guid.Parse(userIdClaim));

                    ViewData["SuccessMessage"] = "Payment was successful, and invoice has been generated!";
                    return View("PaymentSuccess", invoices);
                }
                else
                {
                    ViewData["ErrorMessage"] = "Payment execution failed. Please contact support.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred during payment processing: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult PaymentCancelled()
        {
            ViewData["ErrorMessage"] = "Payment was cancelled.";
            return View("PaymentCancelled");
        }
    }
}
