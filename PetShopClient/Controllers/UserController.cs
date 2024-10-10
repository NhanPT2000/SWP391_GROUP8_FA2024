using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShopClient.Helper;
using PetShopClient.Models;

namespace PetShopClient.Controllers
{
    public class UserController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<UserController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(ILogger<UserController> logger, IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("Profile/{id}")]
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

            return View("Profile", member);
        }

        [HttpGet("Main/{id}")]
        public async Task<IActionResult> Main(Guid? id)
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

            return PartialView("Main", member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile
            (Guid id, [Bind("UserId,UserName,Email,Gender,Address,PhoneNumber,PhoneNumber2")] User member, IFormFile file)
        {
            if (id != member.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Image");
                    string existingFileName = member.UserName + ".jpg";
                    string? newFileName = SaveImage.SaveUploadedFile(file, uploadPath, existingFileName);

                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        member.Profile = newFileName;
                        ViewBag.Message = "Image uploaded successfully!";
                    }
                    else
                    {
                        ViewBag.Message = "Failed to upload image.";
                    }

                    var memberChanged = await _memberService.UpdateMemberAsync(id, member);
                    if (!memberChanged) { throw new DbUpdateConcurrencyException(); }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }

                return RedirectToAction("Index", "Home");
            }

            return PartialView("Main", member);
        }
        [HttpGet]
        public PartialViewResult ChangePassword()
        {
            return PartialView("ChangePassword");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([Bind("OldPassword,NewPassword,ConfirmPassword")] ChangePassword changePassword)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Invalid1");
                var member = await _memberService.GetMemberDetailsAsync(Guid.Parse(HttpContext.Session.GetString("Id")));
                return PartialView("ChangePassword", changePassword);
            }
            if (changePassword.NewPassword == changePassword.ConfirmPassword)
            {
                var changed = await _memberService.ChangePasswordAsync(
                    changePassword.OldPassword,
                    changePassword.NewPassword,
                    Guid.Parse(HttpContext.Session.GetString("Id")));
                if (changed == true) { return RedirectToAction("Index", "Home"); }
                else
                {
                    Console.WriteLine("Invalid2");
                    return PartialView("ChangePassword", changePassword);
                };
            }
            return PartialView("ChangePassword", changePassword);
        }

        [HttpGet]
        public IActionResult GetImage(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", fileName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
        }
    }
}
