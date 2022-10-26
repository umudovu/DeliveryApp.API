using DeliveryApp.Domain.Entities;

namespace DeliveryApp.CourierMVC.Models
{
    public class OrderReturnVM
    {
        public Order Order { get; set; }
        public string DestLat { get; set; }
        public string DestLng { get; set; }
    }
}
