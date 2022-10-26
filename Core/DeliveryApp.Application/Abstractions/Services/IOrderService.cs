
using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface IOrderService
    {
        bool CreateOrder(Order order, List<OrderItem> orderItems);
        bool Pay();
        Task<bool> AddCourierAsync(int? id,int courierId);

        Task<IQueryable<Order>> GetAllAsync();
        Task<Order> GetOrderByIdAsync(int? id);
        Task<bool> ChangeStatus(int id,OrderStatus status);
        Task<bool> CompleteOrder(int orderId,int courierId);



    }
}
