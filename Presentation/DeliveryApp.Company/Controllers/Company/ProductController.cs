using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Company.Controllers.Company
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
