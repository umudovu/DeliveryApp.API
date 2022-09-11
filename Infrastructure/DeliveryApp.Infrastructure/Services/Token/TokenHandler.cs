using DeliveryApp.Application.Abstractions.Token;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Infrastructure.Services.Token
{
	public class TokenHandler:ITokenHandler
	{
		private readonly UserManager<AppUser> _userManager;

		public TokenHandler(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Application.DTOs.Token> CreateAccessTokenAsync(int second, AppUser user)
		{
			Application.DTOs.Token token = new();
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
			};
			var roles = await _userManager.GetRolesAsync(user);

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			var secretkey = "f5c0078b-f6f6-42bc-9e48-c37b6ee4bad9";
			SymmetricSecurityKey key =
					new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretkey));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			token.Expiration = DateTime.UtcNow.AddSeconds(second);

			SecurityTokenDescriptor tokenDescriptor = new ()
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = credentials,
				Issuer = "https://localhost:44379",
				Audience = "https://localhost:44379"
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenS = tokenHandler.CreateToken(tokenDescriptor);

			JwtSecurityTokenHandler TokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(tokenS);

			token.RefreshToken = CreateRefreshToken();
			return token;

		}

		public string CreateRefreshToken()
		{
			byte[] number = new byte[32];
			using RandomNumberGenerator random = RandomNumberGenerator.Create();
			random.GetBytes(number);
			return Convert.ToBase64String(number);
		}
	}
}
