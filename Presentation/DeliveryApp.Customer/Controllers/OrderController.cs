using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("checkout");
        }

        public IActionResult Checkout()
        {

            return View();
        }
    }
}
