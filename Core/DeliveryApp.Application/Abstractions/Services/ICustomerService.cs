using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ICustomerService
    {
        DeliveryApp.Domain.Entities.Customer GetCustomer(string userId);

        Task<bool> UpdatePhotoAsync(IFormFile Photo, string userId);

    }
}
