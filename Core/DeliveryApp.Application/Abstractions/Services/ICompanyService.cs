using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ICompanyService
	{
		Task<CreateUserResponse> CreateAsync(RegisterCompanyVM model);
		Task<bool> UpdateAsync(UpdateCompanyDto companyDto);
		Task<bool> UpdatePhotoAsync(IFormFile Photo,string userId);

		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
		Company GetCompany(string userId);
		IQueryable<Company> GetAllCompany();
		Task<Company> GetCompanyByIdAsync(int id);
	}
}
