using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShopClient.Models;
using System.Diagnostics;

namespace PetShopClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Email, Password")] RequestAccount requestedAccount)
        {
            if (HttpContext.Session.GetString("Id") == null)
            {
                if (ModelState.IsValid)
                {
                    var member = await _memberService.GetMemberByEmailAsync(requestedAccount.Email, requestedAccount.Password);
                    if (member != null)
                    {
                        HttpContext.Session.SetString("Id", member.MemberId.ToString());
                        HttpContext.Session.SetString("MemberName", member.MemberName.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Profile(Guid? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberDetailsAsync((Guid)id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }
    }
}
