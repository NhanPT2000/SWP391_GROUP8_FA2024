using DataAccess.Repository;
using DataAccess.Service;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMemberService _memberService;
        private readonly IEventService _eventService;
        private readonly IPetServiceService _petServiceService;
        private readonly ICategoryService _categoryService;
        private readonly IPlannedServiceService _plannedService;
        private readonly IFacilityService _facilityService;
        private readonly IInvoiceService _invoiceService;
        public AdminController(
            IProductService productService,
            IPetServiceService petServiceService,
            IMemberService memberService,
            IEventService eventService,
            ICategoryService categoryService,
            IPlannedServiceService plannedServiceService,
            IFacilityService facilityService,
            IInvoiceService invoiceService) 
        {
            _productService = productService;
            _petServiceService = petServiceService;
            _memberService = memberService;
            _eventService = eventService;
            _categoryService = categoryService;
            _plannedService = plannedServiceService;
            _facilityService = facilityService;
            _invoiceService = invoiceService;
        }
        public async Task<IActionResult> Main()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                var user = await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
                if (user != null && user._Role.RoleName == "Admin")
                {
                    ViewBag.UserId = userIdClaim;
                    ViewBag.UserName = user.UserName;
                    ViewBag.Role = user._Role.RoleName;
                    return View();
                }
            }
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<ActionResult> ProductSection()
        {
            var products = await _productService.GetProductsAsync();
            return PartialView("ProductSection", products);
        }
        [HttpGet]
        public async Task<IActionResult> UserSection()
        {
            var users = await _memberService.GetMembersAsync();
            return PartialView("UserSection", users);
        }

        [Route("Admin/OverviewSection")]
        [HttpGet]
        public IActionResult OverviewSection()
        {

            return PartialView("OverviewSection");
        }


        [Route("Admin/PetServiceSection")]
        [HttpGet]
        public async Task<IActionResult> PetServiceSection()
        {
            var petServices = await _petServiceService.GetPetServicesAsync();
            return PartialView("PetServiceSection", petServices);
        }

        [Route("Admin/InvoiceSection")]
        [HttpGet]
        public async Task<IActionResult> InvoiceSection()
        {
            var invoices = await _invoiceService.GetInvoicesAsync();
            return PartialView("InvoiceSection", invoices);
        }

        [Route("Admin/FacilitySection")]
        [HttpGet]
        public async Task<IActionResult> FacilitySection()
        {
            var facilities = await _facilityService.GetFacilitiesAsync();
            return PartialView("FacilitySection", facilities);
        }

        [Route("Admin/PlannedServiceSection")]
        [HttpGet]
        public async Task<IActionResult> PlannedServiceSection()
        {
            var services = await _plannedService.GetPlannedServicesAsync();
            return PartialView("PlannedServiceSection", services);
        }

        [Route("Admin/EventSection")]
        [HttpGet]
        public async Task<IActionResult> EventSection()
        {
           
            return PartialView("EventSection", await _eventService.GetAllEventsAsync());
        }

        /*[Route("Admin/InvoiceSection")]
        [HttpGet]
        public async Task<IActionResult> InvoiceSection()
        {
            var invoices = await _invoiceService.GetInvoicesAsync();
            return PartialView("InvoiceSection", invoices);
        }*/

        /*[Route("Admin/IncomeSection")]
        [HttpGet]
        public async Task<IActionResult> IncomeSection()
        {
            var incomeData = await _incomeService.GetIncomeDataAsync();
            return PartialView("IncomeSection", incomeData);
        }*/

        /*[Route("Admin/ProfileSection")]
        [HttpGet]
        public IActionResult ProfileSection()
        {
            var profile = await _profileService.GetAdminProfileAsync();
            return PartialView("ProfileSection", profile);
        }*/

        [HttpGet]
        public async Task<IActionResult> ProductForm()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryName = new SelectList(categories, "CategoryId", "CategoryName");

            return PartialView("ProductForm", new Product());
        }
        [HttpGet]
        public IActionResult FacilityForm()
        {

            return PartialView("FacilityForm", new Facility());
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProductForm(Guid id)
        {

            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.CategoryName = new SelectList(categories, "CategoryId", "CategoryName");

            return PartialView("UpdateProductForm", await _productService.GetProductByIdAsync(id));
        }

        [HttpGet]
        public IActionResult ServiceForm()
        {           
            return PartialView("ServiceForm", new Service());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateServiceForm(Guid id)
        {
            return PartialView("UpdateServiceForm", await _petServiceService.GetServiceByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFacilityForm(Guid id)
        {
            var facility = await _facilityService.GetFacilityByIdAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            return PartialView("UpdateFacilityForm", facility);
        }

        [HttpPost]
        public async Task<IActionResult> AddService(Service service, IFormFile serviceImage)
        {
            if (ModelState.IsValid)
            {
                service.ServiceId = Guid.NewGuid();
                if (serviceImage != null && serviceImage.Length > 0)
                {
                    var fileName = $"{service.ServiceId}{Path.GetExtension(serviceImage.FileName)}";
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Service", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await serviceImage.CopyToAsync(stream);
                    }
                    service.Image = fileName;
                    Console.WriteLine($"File: {service.Image} was added!");
                }
                await _petServiceService.CreatePetServiceAsync(service);

                return Json(new { success = true, message = "Service added successfully!" });
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return Json(new { success = false, message = "Failed to add pet service." });
        }

        [HttpPost]
        public async Task<IActionResult> AddFacility(Facility facility, IFormFile facilityImage)
        {
            if (ModelState.IsValid)
            {
                facility.FacilityId = Guid.NewGuid();
                if (facilityImage != null && facilityImage.Length > 0)
                {
                    var fileName = $"{facility.FacilityId}{Path.GetExtension(facilityImage.FileName)}";
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Facility", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await facilityImage.CopyToAsync(stream);
                    }
                    facility.Image = fileName;
                    Console.WriteLine($"File: {facility.Image} was added!");
                }
                await _facilityService.CreateFacilityAsync(facility);

                return Json(new { success = true, message = "Facility added successfully!" });
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return Json(new { success = false, message = "Failed to add facility." });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile productImage)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                if (productImage != null && productImage.Length > 0)
                {
                    var fileName = $"{product.ProductId}{Path.GetExtension(productImage.FileName)}";
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(stream);
                    }
                    product.Image = fileName;
                }
                await _productService.CreateProductAsync(product);

                return Json(new { success = true, message = "Product added successfully!" });
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return Json(new { success = false, message = "Failed to add product." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return Json(new { success = false, error = "Product not found" });
            if (product.Image != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", product.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            var result = await _productService.DeleteProductAsync(id);
            return Json(new { success = result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var service = await _petServiceService.GetServiceByIdAsync(id);
            if (service == null)
                return Json(new { success = false, error = "Service not found" });
            if (service.Image != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Service",service.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            var result = await _petServiceService.DeleteServiceAsync(id);
            return Json(new { success = result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFacility(Guid id)
        {
            var a = await _facilityService.GetFacilityByIdAsync(id);
            if (a == null)
                return Json(new { success = false, error = "Facility not found" });
            if (a.Image != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Facility", a.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            var result = await _facilityService.DeleteFacilityAsync(id);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromForm] Product product, IFormFile productImage)
        {
            try
            {
                var existingProduct = await _productService.GetProductByIdAsync(product.ProductId);
                Console.WriteLine($"Updating product with ID: {product.ProductId}");
                Console.WriteLine($"Received Product Name: {product.ProductName}");
                if (existingProduct == null)
                {
                    return Json(new { success = false, error = "Product not found" });
                }

                if (productImage != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", existingProduct.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var newFileName = $"{product.ProductId}{Path.GetExtension(productImage.FileName)}";
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", newFileName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(stream);
                    }
                    existingProduct.Image = newFileName;
                }

                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.Origin = product.Origin;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.Weight = product.Weight;
                existingProduct.UnitsInStock = product.UnitsInStock;
                existingProduct.CategoryId = product.CategoryId;

                var result = await _productService.UpdateProductAsync(existingProduct, product.ProductId);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating product: {ex.Message}");
                return Json(new { success = false, error = "An error occurred while updating the product." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService([FromForm] Service service, IFormFile serviceImage)
        {
            try
            {
                var existingService = await _petServiceService.GetServiceByIdAsync(service.ServiceId);
                Console.WriteLine($"Updating service with ID: {service.ServiceId}");
                Console.WriteLine($"Received service Name: {service.ServiceName}");
                if (existingService == null)
                {
                    return Json(new { success = false, error = "Product not found" });
                }

                if (serviceImage != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Service",existingService.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var newFileName = $"{service.ServiceId}{Path.GetExtension(serviceImage.FileName)}";
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Service", newFileName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await serviceImage.CopyToAsync(stream);
                    }
                    existingService.Image = newFileName;
                }

                existingService.ServiceName = service.ServiceName;
                existingService.ServiceDescription = service.ServiceDescription;
                existingService.CostPerUnity = service.CostPerUnity;
                existingService.HasLimit = service.HasLimit;

                var result = await _petServiceService.UpdateServiceAsync(existingService, service.ServiceId);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating service: {ex.Message}");
                return Json(new { success = false, error = "An error occurred while updating the pet service." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFacility([FromForm] Facility facility, IFormFile facilityImage)
        {
            try
            {
                var existingFacility = await _facilityService.GetFacilityByIdAsync(facility.FacilityId);
                Console.WriteLine($"Updating service with ID: {facility.FacilityId}");
                Console.WriteLine($"Received service Name: {facility.FacilityName}");
                if (existingFacility == null)
                {
                    return Json(new { success = false, error = "Facility not found" });
                }

                if (facilityImage != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Facility", existingFacility.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    var newFileName = $"{facility.FacilityId}{Path.GetExtension(facilityImage.FileName)}";
                    var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Facility", newFileName);
                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        await facilityImage.CopyToAsync(stream);
                    }
                    facility.Image = newFileName;
                }

                existingFacility.FacilityName = facility.FacilityName;
                existingFacility.PhoneNumber = facility.PhoneNumber;
                existingFacility.Address = facility.Address;
                existingFacility.ContractPer = facility.ContractPer;
                existingFacility.Image = facility.Image;
                Console.WriteLine(existingFacility.PhoneNumber);

                var result = await _facilityService.UpdateFacilityAsync(existingFacility, facility.FacilityId);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating facility: {ex.Message}");
                return Json(new { success = false, error = "An error occurred while updating the facility." });
            }
        }

        [HttpGet]
        public IActionResult GetServiceImage(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Service", fileName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
        }

        [HttpGet]
        public IActionResult GetFacilityImage(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Facility", fileName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
        }

        [HttpGet]
        public IActionResult GetEventImage(string fileName)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", "Event", fileName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePlannedService(Guid id)
        {
            var service = await _plannedService.GetPlannedServiceByIdAsync(id);
            if (service == null)
                return Json(new { success = false, error = "Service not found" });
            var result = await _plannedService.DeletePlannedServiceAsync(id);
            return Json(new { success = result });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedPlannedService(Guid id)
        {
            var service = await _plannedService.GetPlannedServiceByIdAsync(id);
            if (service == null)
                return Json(new { success = false, error = "Service not found" });
            service.IsCofirmed = true;
            var result = await _plannedService.UpdatePlannedServiceAsync(service,id);
            return Json(new { success = result });
        }
        [HttpGet]
        public async Task<IActionResult> UnconfirmedPlannedService(Guid id)
        {
            var service = await _plannedService.GetPlannedServiceByIdAsync(id);
            if (service == null)
                return Json(new { success = false, error = "Service not found" });
            service.IsCofirmed = false;
            var result = await _plannedService.UpdatePlannedServiceAsync(service, id);
            return Json(new { success = result });
        }

        [HttpGet]
        public IActionResult EventForm()
        {
            return PartialView("EventForm", new Event());
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEventForm(Guid id)
        {
            return PartialView("UpdateEventForm", await _eventService.GetEventByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event eventModel, IFormFile eventImage)
        {
            if (ModelState.IsValid)
            {
                eventModel.EventId = Guid.NewGuid();
                eventModel.AdminId = ViewBag.UserId;
                if (eventImage != null && eventImage.Length > 0)
                {
                    var fileName = $"{eventModel.EventId}{Path.GetExtension(eventImage.FileName)}";
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image/Event", fileName);

                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await eventImage.CopyToAsync(stream);
                    }
                    eventModel.Image = fileName;
                }
                await _eventService.CreateEventAsync(eventModel);

                return Json(new { success = true, message = "Event added successfully!" });
            }
            return Json(new { success = false, message = "Failed to add event." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var eventModel = await _eventService.GetEventByIdAsync(id);
            if (eventModel == null)
                return Json(new { success = false, error = "Event not found" });

            if (eventModel.Image != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image/Event", eventModel.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            var result = await _eventService.DeleteEventAsync(id);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEvent([FromForm] Event eventModel, IFormFile eventImage)
        {
            var existingEvent = await _eventService.GetEventByIdAsync(eventModel.EventId);
            if (existingEvent == null)
            {
                return Json(new { success = false, error = "Event not found" });
            }

            if (eventImage != null)
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image/Event", existingEvent.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                var newFileName = $"{eventModel.EventId}{Path.GetExtension(eventImage.FileName)}";
                var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image/Event", newFileName);
                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await eventImage.CopyToAsync(stream);
                }
                existingEvent.Image = newFileName;
            }

            existingEvent.EventTitle = eventModel.EventTitle;
            existingEvent.Description = eventModel.Description;
            existingEvent.PostDate = eventModel.PostDate;

            var result = await _eventService.UpdateEventAsync(existingEvent, existingEvent.EventId);
            return Json(new { success = result });
        }
    }
}
