using AutoMapper;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanyALl()
        {
            try
            {
                var company = _companyService.GetAllCompany().ToList();
                var response = _mapper.Map<List<CompanyResponse>>(company);

                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("restaurant/{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            var response = _mapper.Map<CompanyResponse>(company);

            return Ok(response);
        }



    }
}
