using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.CourierMVC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
