using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.FileAccess;
using PetShopClient.Models;

namespace PetShopClient.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMemberService _memberService;

        public AccessController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(RequestAccount requestedAccount)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var member = await _memberService.GetMemberByEmailAsync(requestedAccount.Email);
                if (member != null && await _memberService.CheckPassword(requestedAccount.Password))
                {
                    HttpContext.Session.SetString("Email", member.Email.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
