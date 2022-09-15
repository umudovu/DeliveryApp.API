using DeliveryApp.Application.DTOs.Category;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ICategoryService
	{
		Task<bool> AddCategoryAsync(CategoryCreateVM categoryVM,string userId);
		Task<bool> UpdateCategoryAsync(CategoryUpdateVM categoryVM);
		Task<bool> DeleteCategoryAsync(int id);
		Task<Category> GetSingleCategoryAsync(int id);
		IQueryable<Category> GetAllCategory(string userId);
		
	}
}
