using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Enums;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.Persistence.Services
{
	public class UserService : IUserService
	{
		readonly UserManager<AppUser> _userManager;
		private readonly AppDbContext _context;
        public UserService(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
		{

			AppUser user = new()
			{
				UserName = model.Email,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
			};

			IdentityResult result = await _userManager.CreateAsync(user, model.Password);

			DeliveryApp.Domain.Entities.Customer customer = new()
			{
				Name = model.Name,
				SurName = model.SurName,
				Address = model.Address,
				AppUserId = user.Id
			};


			await _userManager.AddToRoleAsync(user, AppRole.Member.ToString());

			CreateUserResponse response = new() { Succeeded = result.Succeeded };

			if (result.Succeeded)
            {
				response.Message = "The user has been successfully created.";
				await _context.AddAsync(customer);
				await _context.SaveChangesAsync();
			}

            else
            {
				foreach (var error in result.Errors)
					response.Message += $"{error.Code} - {error.Description}\n";
			}
				

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
