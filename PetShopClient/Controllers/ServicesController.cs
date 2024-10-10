using Microsoft.AspNetCore.Mvc;

namespace PetShopClient.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
