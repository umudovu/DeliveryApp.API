using DeliveryApp.Application.DTOs.Customers;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.ViewComponents
{
    public class HeaderAuthViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        public HeaderAuthViewComponent(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            CustomerResponse response=new();
            
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                var customer = _context.Customers.FirstOrDefault(x=>x.AppUserId==user.Id);
                response.Name = customer.Name;
            }
            

                return View(response);
        }
    }
}
