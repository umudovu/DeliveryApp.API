using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Customer.Models;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICompanyService _companyService;

        public HomeController(ICategoryService categoryService, ICompanyService companyService)
        {
            _categoryService = categoryService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            IQueryable<Category> categories;
            IQueryable<Company> companies;
            IQueryable<Product> products;
            HomeVM homeVM = new();
            try
            {
                categories = _categoryService.GetAllCategory();
                companies = _companyService.GetAllCompany();

                homeVM.Categories = categories.ToList();
                homeVM.Companies = companies.ToList();
                return View(homeVM);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
