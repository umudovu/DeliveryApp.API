using DeliveryApp.Application.Repositories;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            await _companyRepository.AddAsync(
                new() { Name = "Company1",
                Categories = new HashSet<Category>()
                {
                    new(){Name = "Cate 1"}
                }
                
                });
            await _companyRepository.SaveAsync();

            return Ok(_companyRepository.GetAll());
        }
    }
}
