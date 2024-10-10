using Microsoft.AspNetCore.Mvc;

namespace PetShopClient.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
