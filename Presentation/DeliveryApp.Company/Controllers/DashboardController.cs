using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DeliveryApp.Persistence;
using DeliveryApp.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryApp.Company.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
		public DashboardController(ILogger<DashboardController> logger)
		{
			_logger = logger;
		}
		
		public IActionResult Index()
        {

            return View();
        }

       
    }
}