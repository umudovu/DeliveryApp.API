using DeliveryApp.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<Models.Token> LoginAsync(LoginDto loginDto);
        Task<Models.Token> RefreshTokenLoginAsync(string refreshToken);
        Task<Microsoft.AspNetCore.Identity.SignInResult> LoginMvcAsync(LoginDto loginDto);
        Task<bool> Logout();
    }
}
