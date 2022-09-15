using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Company.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        public HeaderViewComponent(UserManager<AppUser> userManager, ICompanyService companyService)
        {
            _userManager = userManager;
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company =  _companyService.GetCompany(user.Id);

            return View(company);
        }
    }
}
