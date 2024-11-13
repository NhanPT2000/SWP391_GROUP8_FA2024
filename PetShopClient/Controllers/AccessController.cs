using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PetShopClient.Models;
using System.Security.Claims;
using PetShopClient.Helper;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace PetShopClient.Controllers
{
    public class AccessController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly SendEmail _sendEmail;

        public AccessController(IMemberService memberService, SendEmail sendEmail)
        {
            _memberService = memberService;
            _sendEmail = sendEmail;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Id") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email, Password")] RequestAccount requestedAccount, string returnUrl = null)
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

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(requestedAccount);
            }
            return View(requestedAccount);
        } 
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Id") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,Password,Email,Gender,PhoneNumber,PhoneNumber2,Addess")] User member)
        {
            if (ModelState.IsValid)
            {
                /*if(_memberService.GetMemberByEmailOnlyAsync(member.Email)!=null) return View();*/
                member.UserId = new Guid();
                await _memberService.CreateMemberAsync(member);
                await _sendEmail.SendConfirmationEmail(member.Email, member.UserId, Request.Scheme, Request.Host.ToString());
                return RedirectToAction("Login", "Access");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation error: {error.ErrorMessage}");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId)
        {
            var user = await _memberService.GetMemberDetailsAsync(userId);
            if (user != null)
            {
                user.ConfirmedEmail = true;
                var done = await _memberService.UpdateMemberAsync(userId, user);
                ViewData["ShowRegisterConfirmation"] = true;
                return View("Login");
            }
            return RedirectToAction("Index","Home");
        }

        private async Task SendEmail(string email, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("phamnhan.27122000@gmail.com");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("phamnhan.27122000@gmail.com", "kqnw txys wkuo kdts");
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
