using DeliveryApp.Customer.Models;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryApp.Customer.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketViewComponent(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderVM headerVM = new();

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                var customer = _context.Customers.FirstOrDefault(x => x.AppUserId == user.Id);
                headerVM.Customer = new()
                {
                    Name = customer.Name,
                    SurName = customer.SurName,
                    ImageUrl = customer.ImageUrl,
                    Address = customer.Address,
                };
            }

            string basket = Request.Cookies["basket"];
            headerVM.SumVM = new();
            headerVM.BasketVM = new();

            ViewBag.SubTotal = headerVM.SumVM.SubTotal;
            ViewBag.BasketCount = headerVM.SumVM.BasketCount;
            if (basket != null)
            {
            
                headerVM.BasketVM = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

                string sum = Request.Cookies["sum"];
                headerVM.SumVM = JsonConvert.DeserializeObject<SumVM>(sum);
                ViewBag.SubTotal = headerVM.SumVM.SubTotal;
                ViewBag.BasketCount = headerVM.SumVM.BasketCount;
            }

            return View(headerVM);
        }
    }
}
