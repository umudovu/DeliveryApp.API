using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.Repositories;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Persistence.Services
{

    public class CategoryService : ICategoryService
	{
		public readonly ICompanyService _companyService;
		public readonly ICategoryRepository _categoryRepository;
		private readonly IPhotoService _photoService;

        public CategoryService(ICompanyService companyService, ICategoryRepository categoryRepository,
								IPhotoService photoService)
        {
            _companyService = companyService;
            _categoryRepository = categoryRepository;
            _photoService = photoService;
        }

        public async Task<bool> AddCategoryAsync(CategoryCreateVM categoryVM, string userId)
		{
			var company = _companyService.GetCompany(userId);
			var category = GetAllCategory(userId);

			if (category.Any(c => c.Name.ToLower() == categoryVM.Name.ToLower()))
			{
				throw new Exception("Category name is exist!");
			}
			var imageResult = await _photoService.AddPhotoAsync(categoryVM.Photo);
			Category newCategory = new()
			{
				Name = categoryVM.Name,
				CompanyId = company.Id,
				ParentId = categoryVM.ParentId,
				ImageUrl= imageResult.SecureUrl.AbsoluteUri,
				ImagePublicId = imageResult.PublicId
			};
			

			await _categoryRepository.AddAsync(newCategory);
			return await _categoryRepository.SaveAsync();
		}

		public async Task<bool> DeleteCategoryAsync(int id)
		{
			var category = await GetSingleCategoryAsync(id);
			await _photoService.DeletePhotoAsync(category.ImagePublicId);
			_categoryRepository.Remove(category);
			return await _categoryRepository.SaveAsync();
		}

		

		public async Task<bool> UpdateCategoryAsync(CategoryUpdateVM categoryVM)
		{
			var category =await _categoryRepository.GetByIdAsync(categoryVM.Id);
			if (category == null) throw new Exception("Not found");
            if (categoryVM.Photo != null)
            {
				var result = await _photoService.DeletePhotoAsync(category.ImagePublicId);

				if (result.Error != null) throw new Exception(result.Error.Message);

				var imageResult = await _photoService.AddPhotoAsync(categoryVM.Photo);
				category.ImageUrl = imageResult.SecureUrl.AbsoluteUri;
				category.ImagePublicId = imageResult.PublicId;
			}

			var categoryName =await _categoryRepository.GetSingleAsync(x => x.Name.ToLower() == categoryVM.Name.ToLower());

			if (categoryName != null)
			{
				if (category.Name.ToLower() != categoryName.Name.ToLower())
				{
					throw new Exception("Name exsist");
				}
			}
			category.Name = categoryVM.Name;
			category.ParentId=categoryVM.ParentId;
			category.UpdateDate = DateTime.Now;

			_categoryRepository.Update(category);

			return await _categoryRepository.SaveAsync();


		}
		public IQueryable<Category> GetAllCategory(string userId)
		{
			var company = _companyService.GetCompany(userId);
			
			return company.Categories.AsQueryable();
		}

		public async Task<Category> GetSingleCategoryAsync(int id)
		{
			return await _categoryRepository.GetSingleAsync(x => x.Id == id);
		}
	}
}
