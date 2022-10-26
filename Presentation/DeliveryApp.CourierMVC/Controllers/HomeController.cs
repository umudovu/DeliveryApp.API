using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DeliveryApp.CourierMVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ILogger<HomeController> logger, AppDbContext context, IOrderService orderService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            CourierHomeVM homeVM = new();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            homeVM.Courier = _context.Couriers.Include(x=>x.Orders).FirstOrDefault(x => x.AppUserId == user.Id);
            var orders =await _orderService.GetAllAsync();
            orders = orders.Where(x => x.CourierId == homeVM.Courier.Id);

            homeVM.Orders = orders.ToList();



            return View(homeVM);
        }

        
    }
}