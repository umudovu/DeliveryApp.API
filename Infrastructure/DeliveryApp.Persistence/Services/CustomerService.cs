using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
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

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public DeliveryApp.Domain.Entities.Customer GetCustomer(string userId)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.AppUserId == userId);

            return customer;
        }
    }
}
