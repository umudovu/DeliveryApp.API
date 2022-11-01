using DeliveryApp.Application.DTOs.Customers;

namespace DeliveryApp.Customer.Models
{
    public class HeaderVM
    {
        public string CustomerName { get; set; }
        public List<BasketVM> BasketVM { get; set; }
        public CustomerResponse Customer { get; set; }
        public SumVM SumVM { get; set; }
        public DeliveryApp.Domain.Entities.Customer CustomerProfile { get; set; }
    }
}
