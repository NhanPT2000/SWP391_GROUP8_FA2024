using Microsoft.AspNetCore.Mvc;

namespace PetShopClient.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult PlanAService()
        {
            return View();
        }
    }
}
