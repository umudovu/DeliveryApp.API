using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DeliveryApp.Persistence;
using DeliveryApp.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using DeliveryApp.Application.ViewModels;

namespace DeliveryApp.Company.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public DashboardController(ILogger<DashboardController> logger, ICompanyService companyService, UserManager<AppUser> userManager, IProductService productService, IOrderService orderService)
        {
            _logger = logger;
            _companyService = companyService;
            _userManager = userManager;
            _productService = productService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            CompanyDashboardVM dashboardVM = new();
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            dashboardVM.Company = _companyService.GetCompany(user.Id);

            var bestSeller = await _productService.GetBestSellerAsync(user.Id);
            dashboardVM.BestSeller = bestSeller.ToList();

            var x = await _orderService.GetAllAsync();
            x = x.Where(x => x.CompanyId == dashboardVM.Company.Id).Where(x => x.CreatedDate == DateTime.Today);
            ViewBag.DailyOrder = x.Count();
            return View(dashboardVM);
        }

       
    }
}