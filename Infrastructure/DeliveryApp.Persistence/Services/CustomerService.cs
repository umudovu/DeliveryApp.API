using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public CustomerService(AppDbContext context, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _context = context;
            _userManager = userManager;
            _photoService = photoService;
        }

        public DeliveryApp.Domain.Entities.Customer GetCustomer(string userId)
        {
            var customer = _context.Customers.Include(x=>x.AppUser).FirstOrDefault(x => x.AppUserId == userId);

            return customer;
        }

        public async Task<bool> UpdatePhotoAsync(IFormFile Photo, string userId)
        {
            if (Photo != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                var customer = GetCustomer(user.Id);

                if (customer.ImagePublicId != null) await _photoService.DeletePhotoAsync(customer.ImagePublicId);

                var imageResult = await _photoService.AddPhotoAsync(Photo);

                customer.ImageUrl = imageResult.SecureUrl.AbsoluteUri;
                customer.ImagePublicId = imageResult.PublicId;

                return await _context.SaveChangesAsync()>0;
            }
            return false;
        }
    }
}
