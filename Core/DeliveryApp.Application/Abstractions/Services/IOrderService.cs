
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
    }
}
