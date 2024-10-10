using Microsoft.AspNetCore.Mvc;

namespace PetShopClient.Controllers
{
    public class CartController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
