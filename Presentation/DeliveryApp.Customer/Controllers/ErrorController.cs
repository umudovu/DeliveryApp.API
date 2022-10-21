using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Empty()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
