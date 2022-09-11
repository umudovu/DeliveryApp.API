using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Application.ViewModels.Company;
using DeliveryApp.Domain.Entities;
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
		Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
		Task<Company> GetCompanyAsync(string userId);
	}
}
