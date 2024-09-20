using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.FileAccess;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
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
        public async Task<IActionResult> Login([Bind("Email, Password")]RequestAccount requestedAccount)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                if (ModelState.IsValid)
                {
                    var member = await _memberService.GetMemberByEmailAsync(requestedAccount.Email, requestedAccount.Password);
                    if (member != null)
                    {
                        HttpContext.Session.SetString("Email", member.Email.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
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
        public async Task<IActionResult> Register([Bind("MemberName,Password,Email,Gender,PhoneNumber,PhoneNumber2,Addess")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.MemberId = new Guid();
                await _memberService.CreateMemberAsync(member);
                return RedirectToAction("Login", "Access");
            }
           /* else
            {
                string validationErrors = string.Join(",",
                    ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());
                Console.WriteLine(validationErrors);
            }*/
            return View();
        }

    }
}
