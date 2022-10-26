using DeliveryApp.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ICourierService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);

    }
}
