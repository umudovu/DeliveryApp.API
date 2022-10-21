using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence.Services
{
    public class OrderService : IOrderService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly AppDbContext _context;

        public OrderService(UserManager<AppUser> userManager, ICustomerService customerService, AppDbContext context)
        {
            _userManager = userManager;
            _customerService = customerService;
            _context = context;
        }

        public bool CreateOrder(Order order, List<OrderItem> orderItems)
        {
            _context.Orders.Add(order);
            _context.OrderItems.AddRange(orderItems);
            

            return _context.SaveChanges()>0;

        }

        public bool Pay()
        {
            throw new NotImplementedException();
        }
    }
}
