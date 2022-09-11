using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Application.Repositories;
using DeliveryApp.Application.ViewModels.Company;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Domain.Entities.Photo;
using DeliveryApp.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICompanyRepository _companyRepository;
		private readonly IPhotoService _photoService;
		public CompanyService(UserManager<AppUser> userManager, ICompanyRepository companyRepository, IPhotoService photoService)
		{
			_userManager = userManager;
			_companyRepository = companyRepository;
			_photoService = photoService;
		}

		public async Task<CreateUserResponse> CreateAsync(RegisterCompanyVM model)
		{
			var imageResult = await _photoService.AddPhotoAsync(model.Photo);
			AppUser user = new()
			{
				Email = model.Email,
				UserName = model.Name.Split(" ")[0].ToLower(),
				Name = model.Name,
				SurName = model.Name,
				ImageUrl= imageResult.SecureUrl.AbsoluteUri,
				ImagePublicId = imageResult.PublicId
			};

			IdentityResult result = await _userManager.CreateAsync(user,model.Password);

			bool companyResult = await _companyRepository.AddAsync(new()
			{
				Name = model.Name,
				Adress = model.Adress,
				PhoneNumber = model.PhoneNumber,
				AppUserId = user.Id,
				StartJob =model.StartJob,
				EndJob = model.EndJob,
			});

			
			//f (result.Error != null) return BadRequest(result.Error.Message);

			bool companySaveResult =await _companyRepository.SaveAsync();

			await _userManager.AddToRoleAsync(user, AppRole.Company.ToString());

			CreateUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded&& companyResult&&companySaveResult)
				response.Message = "The user has been successfully created.";
			else
				foreach (var error in result.Errors)
					response.Message += $"{error.Code} - {error.Description}\n";


			return response;
			
		}

        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
		{
			throw new NotImplementedException();
		}


		public Task<Company> GetCompanyAsync(string userId)
		{
			return _companyRepository.GetSingleAsync(x => x.AppUserId == userId);
		}

	}
}
