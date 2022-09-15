using AutoMapper;
using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DeliveryApp.Company.Controllers.Company
{
    public class CategoryController : BaseController
	{

		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var categories =  _categoryService.GetAllCategory(userid);

            //var categories = query.ToList();
            //var c = categories.FirstOrDefault(x => x.Id == 1);
            //var p = c.Photo.Url;

            //ViewBag.Parents = categories.Where(x => x.ParentId == null);

            return View(categories.ToList());
		}


		public async Task<IActionResult> Create()
		{
			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var categories = _categoryService.GetAllCategory(userid);

			
			var parent = categories.Where(x => x.ParentId == null).AsEnumerable();
			ViewBag.Categories =  new SelectList(parent, "Id", "Name"); 

			
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            try
            {
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var categorys =  _categoryService.GetAllCategory(userId).ToList();


				if (!ModelState.IsValid) return View();
				await _categoryService.AddCategoryAsync(category, userId);
			}
            catch (Exception ex)
            {

				ModelState.AddModelError("Name",ex.Message);
				return View();
            }

			return RedirectToAction("Index");
        }

		public async Task<IActionResult> Update(int? id)
		{
			if (id == null) return NotFound();

			var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var categories =  _categoryService.GetAllCategory(userid);

			var category = categories.FirstOrDefault(x=>x.Id==id);
			CategoryUpdateVM updateVM = _mapper.Map<CategoryUpdateVM>(category);
			

			if (category == null) return NotFound();

			var parent = categories.Where(x => x.ParentId == null).AsEnumerable();
			ViewBag.Categories = new SelectList(parent, "Id", "Name");

			return View(updateVM);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(CategoryUpdateVM updateVM)
        {

            try
            {
				await _categoryService.UpdateCategoryAsync(updateVM);
			}
            catch (Exception ex)
            {

				ModelState.AddModelError("Name", ex.Message);
				return View();
			}

			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int id)
        {
            try
            {
				if (id == null) return BadRequest();
				await _categoryService.DeleteCategoryAsync(id);
			}
            catch (Exception ex)
            {
				return BadRequest(ex.Message);
            }
			return RedirectToAction("index");
        }

	}
}
