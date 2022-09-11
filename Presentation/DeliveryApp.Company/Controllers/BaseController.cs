using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Company.Controllers
{
	[Authorize(Roles ="Admin,Company")]
	public class BaseController : Controller
	{
	}
}
