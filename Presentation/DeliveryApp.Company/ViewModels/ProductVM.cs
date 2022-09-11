using DeliveryApp.Domain.Entities;

namespace DeliveryApp.Company.ViewModels
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
