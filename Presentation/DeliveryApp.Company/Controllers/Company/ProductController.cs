using AutoMapper;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.ViewModels.Product;
using DeliveryApp.Company.ViewModels;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DeliveryApp.Company.Controllers.Company
{
	public class ProductController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            _categoryService = categoryService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
		{
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products =  await _productService.GetAllAsync(userid);
            var categories = await _categoryService.GetAllCategoryAsync(userid);
            
            ProductVM productVM = new()
            {
                Products =products.ToList(),
                Categories = categories.ToList()
            };
            var product = await _productService.GetAllAsync(userid);
			return View(productVM);
		}


		public async Task<IActionResult> Create()
        {
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var categories = await _categoryService.GetAllCategoryAsync(userid);


			var children = categories.Where(x => x.ParentId != null).AsEnumerable();
			ViewBag.Categories = new SelectList(children, "Id", "Name");

			return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductCreateVM createVM)
        {
            if(!ModelState.IsValid)
            {
                return View(createVM);
            }
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                await _productService.AddAsync(createVM, userid);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

			return RedirectToAction("index");
        }


        public async Task<IActionResult> Update(int id)
        {
            Product product;
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _categoryService.GetAllCategoryAsync(userid);

            try
            {
                product = await _productService.GetSingleAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            var children = categories.Where(x => x.ParentId != null).AsEnumerable();
            ViewBag.Categories = new SelectList(children, "Id", "Name");
            var productVm = _mapper.Map<ProductUpdateVM>(product);
            return View(productVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Update(ProductUpdateVM updateVM)
        {
            try
            {
                await _productService.UpdateAsync(updateVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           return  RedirectToAction("index");
        }

        public async Task<IActionResult> Remove(int id,string ReturnUrl)
        {
            try
            {
                await _productService.DeleteAsync(id);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Redirect(ReturnUrl);
        }

	}
}
