using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Successful()
        {
            return View();
        }
    }
}
