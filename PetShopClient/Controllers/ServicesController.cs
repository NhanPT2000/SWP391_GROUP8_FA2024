using DataAccess.Repository;
using DataAccess.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class ServicesController : Controller
    {
        private IPetServiceService _petServiceService;
        private IMemberService _memberService;
        public ServicesController(IPetServiceService petServiceService, IMemberService memberService) 
        {
            _petServiceService = petServiceService;
            _memberService = memberService;
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
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ServiceList()
        {
            var services = await _petServiceService.GetPetServicesAsync();
            if (services == null)
            {
                Console.WriteLine("Null list!");
                return RedirectToAction("Index", "Home");
            }
            return PartialView("ServiceList", services);
        }
    }
}
