using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Customer.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly AppDbContext _context;

        public CompanyController(ICompanyService companyService, UserManager<AppUser> userManager, 
                        ICustomerService customerService, AppDbContext context)
        {
            _companyService = companyService;
            _userManager = userManager;
            _customerService = customerService;
            _context = context;
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

        public async Task<IActionResult> Restaurant(int? id)
        {
            if (id == null) return RedirectToAction("empty", "error");
            try
            {
                var company =await _companyService.GetCompanyByIdAsync(id);

                return View(company);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddComment(int restId,string content)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "error");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var customer = _customerService.GetCustomer(user.Id);

            Comment comment = new()
            {
                CustomerId=customer.Id,
                CompanyId=restId,
                Content=content
            };
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return PartialView("_PartialComment", comment);
        }
    }
}
