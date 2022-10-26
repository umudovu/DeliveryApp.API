using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Company.Controllers
{
    public class OrderController : BaseController
    {
        public readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;
        public readonly ICompanyService _companyService;
        public OrderController(IOrderService orderService, ICompanyService companyService, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _companyService = companyService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = _companyService.GetCompany(user.Id);

            var orders = await _orderService.GetAllAsync();
            orders = orders.Where(x => x.CompanyId == company.Id).OrderBy(x=>x.CreatedDate);

            return View(orders.ToList());
        }


        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            return View(order);
        }

        public async Task<IActionResult> Status(int id,OrderStatus status)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            try
            {
                await _orderService.ChangeStatus(id, status);
            }
            catch (Exception)
            {

                throw;
            }
            
            return RedirectToAction("index");
        }

        public async Task<IActionResult> OrderByStatus(OrderStatus status)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = _companyService.GetCompany(user.Id);

            var orders = await _orderService.GetAllAsync();
            orders = orders.Where(x => x.CompanyId == company.Id);
            orders = orders.Where(x => x.OrderStatus == status).OrderBy(x => x.CreatedDate);

            return View(orders.ToList());
        }
    }
}
