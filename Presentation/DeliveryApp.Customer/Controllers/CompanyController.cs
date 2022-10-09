using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            try
            {
                IQueryable<Company> companies = _companyService.GetAllCompany();
                var response = companies.OrderBy(x => x.CreatedDate).ToList();
                return View(response);

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        public async Task<IActionResult> Restaurant(int id)
        {
            try
            {
                var company =await _companyService.GetCompanyByIdAsync(id);

                return View(company);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
