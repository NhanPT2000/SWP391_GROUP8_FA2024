using DataAccess.Repository;
using DataObject;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetShopClient.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMemberService _memberService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ILogger<ProductsController> logger, IProductService productService, IWebHostEnvironment webHostEnvironment,
            IMemberService memberService)
        {
            _logger = logger;
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
            _memberService = memberService;
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
        [HttpGet("Products/Details/{id}")]
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
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(id);
            return View(product);
        }
        //test
        [HttpGet("Products/DetailsTest/{id}")]
        public async Task<IActionResult> DetailsTest(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(id);
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> ProductList() 
        {
            var products = await _productService.GetProductsAsync();
            if(products == null) {
                Console.WriteLine("Null list!");
                return RedirectToAction("Index", "Home"); }
            return PartialView("ProductList", products);
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
