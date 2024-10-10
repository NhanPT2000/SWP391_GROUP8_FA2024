using Microsoft.AspNetCore.Mvc;

namespace PetShopClient.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
