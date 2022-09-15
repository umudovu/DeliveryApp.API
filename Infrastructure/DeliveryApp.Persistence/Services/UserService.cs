using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Persistence.Services
{
	public class UserService : IUserService
	{
		readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				UserName = model.Username,
				Email = model.Email,
			}, model.Password);

			Customer customer = new()
			{
				Name = model.Name,
				SurName = model.SurName,
				Address = model.Address,
			};

			CreateUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
				response.Message = "The user has been successfully created.";
			else
				foreach (var error in result.Errors)
					response.Message += $"{error.Code} - {error.Description}\n";

			return response;

		}



        public async Task UpdateRefreshToken(string refreshToken, AppUser user, 
			DateTime accessTokenDate, int addOnAccessTokenDate)
		{
			if (user != null)
			{
				//user.RefreshToken = refreshToken;
				//user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
				//await _userManager.UpdateAsync(user);
			}
			else
				throw new Exception("User Not Found");
		}
	}
}
