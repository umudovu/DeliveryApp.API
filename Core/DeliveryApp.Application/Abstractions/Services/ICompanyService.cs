using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ICompanyService
	{
		Task<CreateUserResponse> CreateAsync(RegisterCompanyVM model);
		Task<bool> UpdateAsync(UpdateCompanyDto companyDto);
		Task<bool> SetAddress(AddressVM address,int id);
		Task<bool> UpdatePhotoAsync(IFormFile Photo,string userId);

		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
		DeliveryApp.Domain.Entities.Company GetCompany(string userId);
		IQueryable<DeliveryApp.Domain.Entities.Company> GetAllCompany();
		Task<DeliveryApp.Domain.Entities.Company> GetCompanyByIdAsync(int? id);
	}
}
