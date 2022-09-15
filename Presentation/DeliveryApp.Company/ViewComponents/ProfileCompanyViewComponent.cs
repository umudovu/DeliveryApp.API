using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Company.ViewComponents
{
    public class ProfileCompanyViewComponent:ViewComponent
    {
        private readonly ICompanyService _companyService;
        readonly UserManager<AppUser> _userManager;

        public ProfileCompanyViewComponent(ICompanyService companyService, UserManager<AppUser> userManager)
        {
            _companyService = companyService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            var company = _companyService.GetCompany(user.Id);

            UpdateCompanyDto updateCompany = new()
            {
                Id=user.Id,
                Name=company.Name,
                Description=company.Description,
                Adress=company.Adress,
                PhoneNumber=company.PhoneNumber,
                StartJob=company.StartJob,
                EndJob=company.EndJob,
            };
            return View(updateCompany);
        }


    }
}
