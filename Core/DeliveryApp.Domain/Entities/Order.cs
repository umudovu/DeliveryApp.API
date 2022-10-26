using DeliveryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? InvoiceNo { get; set; }
        public string? TrackingNo { get; set; }
        public string? Address { get; set; }
        public string? LatCoord { get; set; }
        public string? LngCoord { get; set; }
        public string? Email { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Nullable<ShippedStatus> ShippedStatus { get; set; }


        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public Courier? Courier { get; set; }
        public Nullable<int> CourierId { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        Unpaid,
        Processing,
        Shipped,
        Completed,
        Canceled,
    }
    public enum ShippedStatus
    {
        Curyer_appointed,
        The_order_is_in_the_courier,
        The_order_has_been_delivered
    }
}
