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
        Task<DTOs.Token> LoginAsync(LoginDto loginDto);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
        Task<bool> Logout();
    }
}
