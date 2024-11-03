using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShopClient.Helper;
using PetShopClient.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task <IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                Guid userId = Guid.Parse(userIdClaim);
                var member = await _memberService.GetMemberDetailsAsync(userId);
                if (member != null)
                {
                    ViewBag.UserId = userId;
                    ViewBag.UserName = member.UserName;
                    ViewBag.Role = member._Role.RoleName;
                    return View();
                }
            }
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
            if (ModelState.IsValid)
            {
                var member = await _memberService.GetMemberByEmailAsync(requestedAccount.Email, requestedAccount.Password);
                if (member != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, member.UserName),
                        new Claim(ClaimTypes.Email, member.Email),
                        new Claim(ClaimTypes.Role, member.RoleId.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, member.UserId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(requestedAccount);
            }
            return View(requestedAccount);
        }
    }
}
