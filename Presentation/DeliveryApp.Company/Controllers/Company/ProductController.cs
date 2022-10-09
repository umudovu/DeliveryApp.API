using AutoMapper;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Company.ViewModels;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index(ProductAction act=ProductAction.IsActive)
		{
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products =  _productService.GetAll(userid);
            products = products.Include(x => x.Category);
            List<Product> list;
            switch (act)
            {
                case ProductAction.IsActive:
                    list = products.Where(x => !x.IsDelete).ToList();
                    break;
                case ProductAction.IsPassive:
                    list=products.Where(x => x.IsDelete).ToList();
                    break;
                default:
                    list = new();
                    break;
            }
            
			return View(list);
		}


		public async Task<IActionResult> Create()
        {
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var categories = _categoryService.GetAllCategory(userid);


			var children = categories.Where(x => x.ParentId == null).AsEnumerable();
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
            var categories = _categoryService.GetAllCategory(userid);

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
