using DataAccess.Repository;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMemberService _memberService;
        public EventsController(IMemberService memberService, IEventService eventService) 
        {
            _memberService = memberService;
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Main()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                var user = await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
                if (user != null)
                {
                    ViewBag.UserId = userIdClaim;
                    ViewBag.UserName = user.UserName;
                    ViewBag.Role = user._Role.RoleName;
                }
            }
            return View();
        }
        [HttpGet("Events/Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                var user = await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
                if (user != null)
                {
                    ViewBag.UserId = userIdClaim;
                    ViewBag.UserName = user.UserName;
                    ViewBag.Role = user._Role.RoleName;
                }
            }
            var e = await _eventService.GetEventByIdAsync(id);
            if (e == null) return NotFound(id);
            return View(e);
        }

        [HttpGet]
        public async Task<IActionResult> EventList()
        {
            var e = await _eventService.GetAllEventsAsync();
            if (e == null)
            {
                Console.WriteLine("Null list!");
                return RedirectToAction("Index", "Home");
            }
            return PartialView("EventList", e);
        }
    }
}
