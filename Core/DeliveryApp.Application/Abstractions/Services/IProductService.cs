using DeliveryApp.Application.ViewModels;
using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<bool> AddAsync(ProductCreateVM createVM, string userId);
        Task<bool> UpdateAsync(ProductUpdateVM updateVM);
        Task<bool> DeleteAsync(int id);
        Task<Product>GetSingleAsync(int id);
        IQueryable<Product> GetAll(string userId);
    }
}
