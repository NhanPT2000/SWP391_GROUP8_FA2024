using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShopClient.Helper;
using PetShopClient.Models;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class UserController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly ILogger<UserController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPetService _petService;
        private readonly ISpeciesService _speciesService;
        private readonly IPlannedServiceService _plannedService;
        public UserController(
            ILogger<UserController> logger, 
            IMemberService memberService, 
            IWebHostEnvironment webHostEnvironment, 
            IPetService petService,
            ISpeciesService speciesService,
            IPlannedServiceService plannedServiceService)
        {
            _logger = logger;
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
            _petService = petService;
            _speciesService = speciesService;
            _plannedService = plannedServiceService;
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

        [HttpGet]
        public async Task<IActionResult> Main()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberDetailsAsync(Guid.Parse(id));
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
                return PartialView("ChangePassword", changePassword);
            }
            if (changePassword.NewPassword == changePassword.ConfirmPassword)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null) {
                    var changed = await _memberService.ChangePasswordAsync(
                        changePassword.OldPassword,
                        changePassword.NewPassword,
                        Guid.Parse(userIdClaim));
                    if (changed == true) { return RedirectToAction("Index", "Home"); } }
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

        [HttpGet]
        public async Task<IActionResult> PlannedServiceList()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return NotFound();
            }
            var services = await _plannedService.GetPlannedServicesByIdAsync(Guid.Parse(id));
            return PartialView("PlannedServiceList", services);

        }

        [HttpGet]
        public IActionResult GetPetImage(string fileName)
        {
            string imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Pets");
            string imagePath = Path.Combine(imageFolderPath, fileName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> PetList()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) { return RedirectToAction("Login","Access"); }
            var pets = await _petService.GetPetsAsync(Guid.Parse(userIdClaim));
            return PartialView("PetList", pets);
        }
        [HttpGet]
        public async Task<IActionResult> PetForm()
        {
            ViewData["SpeciesId"] = new SelectList(await _speciesService.GetAll(), "SpeciesId", "SpeciesName");
            return PartialView("PetForm");
        }

        [HttpDelete("User/CancelService/{id}")]
        public async Task<IActionResult> CancelService(Guid id)
        {
            try
            {
                var ps = await _plannedService.GetPlannedServiceByIdAsync(id);
                if (ps == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Service not found."
                    });
                }

                var isDeleted = await _plannedService.DeletePlannedServiceAsync(id);
                if (!isDeleted)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Planned Service is not found or could not be deleted."
                    });
                }
                return Json(new
                {
                    success = true,
                    message = "Planned Service deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while deleting your planned service.",
                    error = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PetForm([Bind("PetName,SpeciesId,Birthdate,Notes,Image")] Pet pet, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid || file == null || file.Length == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                        {
                            Console.WriteLine("ModelState error: " + error.ErrorMessage);
                        }
                    }
                    if (file == null)
                    {
                        Console.WriteLine("File is null.");
                    }
                    else if (file.Length == 0)
                    {
                        Console.WriteLine("File length is zero.");
                    }
                    ViewData["SpeciesId"] = new SelectList(await _speciesService.GetAll(), "SpeciesId", "SpeciesName");
                    return PartialView("PetForm", pet);
                }
                var fileExtension = Path.GetExtension(file.FileName);
                var newFileName = $"{pet.PetId}_{Guid.NewGuid()}{fileExtension}";
                var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Pets");

                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }

                var filePath = Path.Combine(imageFolderPath, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await file.CopyToAsync(stream);
                }
                pet.PetId = Guid.NewGuid();
                var memberid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                pet.Image = newFileName;
                pet.MemberId = memberid;
                await _petService.CreatePetAsync(pet);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errror here 2");
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }


                return Json(new { success = false, message = "An error occurred while adding the pet.", error = ex.Message });
            }
        }
    }
}
