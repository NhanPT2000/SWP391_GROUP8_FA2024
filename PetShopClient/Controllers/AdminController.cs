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
        public AdminController(
            IProductService productService,
            IPetServiceService petServiceService,
            IMemberService memberService,
            IEventService eventService,
            ICategoryService categoryService) 
        {
            _productService = productService;
            _petServiceService = petServiceService;
            _memberService = memberService;
            _eventService = eventService;
            _categoryService = categoryService;
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
            // Return the Overview partial view (data if needed)
            return PartialView("OverviewSection");
        }


        [Route("Admin/PetServiceSection")]
        [HttpGet]
        public async Task<IActionResult> PetServiceSection()
        {
            var petServices = await _petServiceService.GetPetServicesAsync();
            return PartialView("PetServiceSection", petServices);
        }


        [Route("Admin/EventSection")]
        [HttpGet]
        public async Task<IActionResult> EventSection()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null)
            {
                var user = await _memberService.GetMemberDetailsAsync(Guid.Parse(userIdClaim));
                if (user != null && user._Role.RoleName == "Admin")
                {
                    var events = await _eventService.GetEventsAsync(Guid.Parse(userIdClaim));
                    return PartialView("EventSection", events);
                }
            }
            return PartialView("EventSection");
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
    }
}
