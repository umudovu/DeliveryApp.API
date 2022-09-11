using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.Abstractions.Token;
using DeliveryApp.Application.DTOs;
using DeliveryApp.Application.DTOs.User;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
	public class AuthService : IAuthService
	{
		readonly UserManager<AppUser> _userManager;
		readonly SignInManager<AppUser> _signInManager;
		readonly IUserService _userService;
		readonly ITokenHandler _tokenHandler;
		public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
			IUserService userService, ITokenHandler tokenHandler)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_userService = userService;
			_tokenHandler = tokenHandler;
		}

		public async Task<Token> LoginAsync(LoginDto loginDto)
		{
			AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
				throw new Exception("User Not Found");

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
			await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, true);
			if (result.Succeeded) 
			{
				Token token =await _tokenHandler.CreateAccessTokenAsync(15,user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
				return token;
			}

			throw new Exception("Authentication error");
		}


		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
			AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
			{
				Token token =await _tokenHandler.CreateAccessTokenAsync(15, user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
				return token;
			}
			else
				throw new Exception("User Not Found");
		}

		public async Task<bool> Logout()
		{
		  await _signInManager.SignOutAsync();
			return true;

		}
	}
}
