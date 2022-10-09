using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Customer.Models
{
    public class HomeVM
    {
        public List<Category>? Categories { get; set; }
        public List<Company>? Companies { get; set; }
        public List<Product>? Products { get; set; }
    }
}
