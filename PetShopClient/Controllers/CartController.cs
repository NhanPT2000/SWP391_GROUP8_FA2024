using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IMemberService _memberService;

        public CartController(IOrderService orderService, IOrderDetailsService orderDetailsService, IMemberService memberService)
        {
            _orderService = orderService;
            _orderDetailsService = orderDetailsService;
            _memberService = memberService;
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
            ViewData["User"] = _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
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

            var newOrder = new Order
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

    }
    }
