using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddCourierAsync(int? id, int courierId)
        {
            var order = await _context.Orders.FindAsync(id);
            order.CourierId = courierId;
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> ChangeStatus(int id, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(id);
            order.OrderStatus = status;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CompleteOrder(int orderId, int courierId)
        {
            var order = await GetOrderByIdAsync(orderId);
            var courier =await _context.Couriers.FindAsync(courierId);

            courier.Balance += order.Company.ServiceFee;
            courier.TotalProfit += order.Company.ServiceFee;
            courier.TotalOrder++;
            order.OrderStatus = OrderStatus.Completed;

            courier.IsActive = false;
            courier.ActiveOrderId = null;

            return await _context.SaveChangesAsync()>0;
        }

        public bool CreateOrder(Order order, List<OrderItem> orderItems)
        {
            _context.Orders.Add(order);
            _context.OrderItems.AddRange(orderItems);
            

            return _context.SaveChanges()>0;

        }

        public async Task<IQueryable<Order>> GetAllAsync()
        {
            var query = _context.Orders.Include(x=>x.Company)
                                       .Include(x=>x.Courier)
                                       .Include(x=>x.Customer)
                                       .Include(x => x.OrderItems)
                                       .ThenInclude(x=>x.Product).OrderBy(x => x.CreatedDate);
            return query;
        }

        public async Task<Order> GetOrderByIdAsync(int? id)
        {
            var order = await _context.Orders.Include(x => x.Company)
                                             .Include(x => x.Customer)
                                             .Include(x => x.Courier)
                                             .Include(x => x.OrderItems)
                                             .ThenInclude(x=>x.Product).FirstOrDefaultAsync(x=>x.Id==id);

            return order;

        }

        public bool Pay()
        {
            throw new NotImplementedException();
        }
    }
}
