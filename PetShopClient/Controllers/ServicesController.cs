using DataAccess.Repository;
using DataAccess.Service;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class ServicesController : Controller
    {
        private IPetServiceService _petServiceService;
        private IMemberService _memberService;
        private IPetService _petService;
        private IFacilityService _facilityService;
        private ICaseService _caseService;
        private IPlannedServiceService _plannedServiceService;
        public ServicesController(IPetServiceService petServiceService, 
            IMemberService memberService, 
            IPetService petService, 
            IFacilityService facilityService,
            ICaseService caseService,
            IPlannedServiceService plannedServiceService) 
        {
            _petServiceService = petServiceService;
            _memberService = memberService;
            _petService = petService;
            _facilityService = facilityService;
            _caseService = caseService;
            _plannedServiceService = plannedServiceService;
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

        [HttpGet("Services/Details/{id}")]
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
                }
            }
            var service = await _petServiceService.GetServiceByIdAsync(id);
            if (service == null) return NotFound(id);
            return View("Details", service);
        }

        [HttpGet]
        public async Task<IActionResult> SelectPet(Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { return RedirectToAction("Login","Access"); }
            var pets = await _petService.GetPetsAsync(Guid.Parse(userId));

            ViewBag.ServiceId = serviceId;
            if (pets == null || !pets.Any())
            {
                return View("NoPet");
            }

            return View("SelectPet", pets);
        }

        [HttpGet]
        public async Task<IActionResult> SelectFacility(Guid petId, Guid serviceId)
        {
            var facilities = await _facilityService.GetFacilitiesAsync();
            if (facilities == null || !facilities.Any())
            {

                Console.WriteLine("No facilities found.");
                return View("SelectFacility", Enumerable.Empty<Facility>());
            }
            ViewBag.PetId = petId;
            ViewBag.ServiceId = serviceId;
            ViewBag.PetId = petId;
            return View("SelectFacility", facilities);
        }

        [HttpGet]
        public IActionResult NoPet()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AssignService(Guid petId, Guid facilityId, Guid serviceId)
        {
            var pet = await _petService.GetPetByIdAsync(petId);
            if (pet == null) return View("NoPet");
            var facil = await _facilityService.GetFacilityByIdAsync(facilityId);
            if (facil == null) return NotFound();
            ViewBag.PetImage = pet.Image;
            ViewBag.PetName = pet.PetName;
            ViewBag.FacilName = facil.FacilityName;
            ViewBag.FacilImage = facil.Image;
            ViewBag.PetId = petId;
            ViewBag.FacilId = facilityId;
            ViewBag.FacilPer = facil.ContractPer;
            ViewBag.Address = facil.Address;
            ViewBag.Phone = facil.PhoneNumber;
            ViewBag.ServiceId = serviceId;
            return View(new Case());
        }

        [HttpPost]
        public async Task<IActionResult> AssignService(Case @case, Guid serviceId, Guid petId)
        {
            if (ModelState.IsValid) 
            {
                @case.CaseId = Guid.NewGuid();
                await _caseService.CreateCaseAsync(@case);
                var pet = await _petService.GetPetByIdAsync(petId);
                var @service = await _petServiceService.GetServiceByIdAsync(serviceId);
                ViewBag.CaseId = @case.CaseId;
                ViewBag.ServiceId = serviceId;
                ViewBag.PetImage = pet.Image;
                ViewBag.PetName = pet.PetName;
                ViewBag.ServicePrice = service.CostPerUnity;
                ViewBag.ServiceName = service.ServiceName;
                ViewBag.ServiceImage = service.Image;
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
                return View("ConfirmService");
            }
            return View(@case);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmServiceAsync()
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
            return View(new PlannedService());
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmService(PlannedService plannedService)
        {
            if (!ModelState.IsValid)
            {
                return View(plannedService);
            }
            plannedService.PlannedServiceId = Guid.NewGuid();
            plannedService.Price = plannedService.CostPerUnit * plannedService.PlannedUnits;
            plannedService.StartTime = DateTime.UtcNow;
            await _plannedServiceService.CreatePlannedServiceAsync(plannedService);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlannedService(Guid petId, Guid facilityId, Guid serviceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newCase = new Case
            {
                CaseId = Guid.NewGuid(),
                PetId = petId,
                FacilityId = facilityId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                IsClosed = false,
                Notes = "Initial case for planned service",
                IsDeleted = false
            };

            await _caseService.CreateCaseAsync(newCase);


            var plannedService = new PlannedService
            {
                PlannedServiceId = Guid.NewGuid(),
                MemberId = Guid.Parse(userId),
                CaseId = newCase.CaseId,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                PlannedUnits = 1,
                CostPerUnit = 5000.00f,
                Price = 5000.00f,
                IsDeleted = false,
                ServiceId = serviceId
            };

            await _plannedServiceService.CreatePlannedServiceAsync(plannedService);

            return RedirectToAction("Index", "Home");
        }
    }
}
