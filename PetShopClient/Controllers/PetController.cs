using DataAccess.Repository;
using DataAccess.Service;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IMemberService _memberService;
        private readonly ISpeciesService _speciesService;
        public PetController(
            IPetService petService, 
            IMemberService memberService, 
            ISpeciesService speciesService
            )
        {
            _petService = petService;
            _memberService = memberService;
            _speciesService = speciesService;
        }
        [HttpGet("Pet/Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) 
            {
                return RedirectToAction("Login","Access");
            }
            ViewBag.UserId = userIdClaim;
            ViewBag.UserName = (await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim))).UserName;
            var pet = await _petService.GetPetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewBag.SpeciesList = new SelectList(await _speciesService.GetAll(), "SpeciesId", "SpeciesName", pet.SpeciesId);

            return View("Details", pet);
        }
        [HttpPost("Pet/Details/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(Guid id, [Bind("PetName,SpeciesId,Birthdate,Notes,Image")] Pet updatedPet, IFormFile? newImageFile)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }

                ViewBag.SpeciesList = new SelectList(await _speciesService.GetAll(), "SpeciesId", "SpeciesName", updatedPet.SpeciesId);
                return Json(new { success = false, message = "Invalid data." });
            }

            try
            {
                if (newImageFile != null && newImageFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(newImageFile.FileName);
                    var newFileName = $"{updatedPet.PetId}_{Guid.NewGuid()}{fileExtension}";
                    var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Pets");

                    var oldImagePath = Path.Combine(imageFolderPath, updatedPet.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var newImagePath = Path.Combine(imageFolderPath, newFileName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await newImageFile.CopyToAsync(stream);
                    }

                    updatedPet.Image = newFileName;
                }

                var success = await _petService.UpdatePetAsync(updatedPet, id);
                if (!success)
                {
                    return Json(new { success = false, message = "Failed to update pet." });
                }

                return Json(new { success = true, message = "Pet updated successfully.", newImageFileName = updatedPet.Image });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while updating the pet.", error = ex.Message });
            }
        }
        [HttpDelete("Pet/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var pet = await _petService.GetPetByIdAsync(id);
                if (pet == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Pet not found."
                    });
                }
                var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Pets");
                var imagePath = Path.Combine(imageFolderPath, pet.Image);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                var isDeleted = await _petService.DeletePetAsync(id);
                if (!isDeleted)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Pet is not found or could not be deleted."
                    });
                }
                return Json(new
                {
                    success = true,
                    message = "Pet deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "An error occurred while deleting your pet.",
                    error = ex.Message
                });
            }
        }
    }
}
