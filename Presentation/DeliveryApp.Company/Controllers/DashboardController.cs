using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DeliveryApp.Persistence;
using DeliveryApp.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Company.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ICompanyService _companyService;
        readonly UserManager<AppUser> _userManager;
        public DashboardController(ILogger<DashboardController> logger, ICompanyService companyService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _companyService = companyService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var company = _companyService.GetCompany(user.Id);


            return View(company);
        }

       
    }
}