using DeliveryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Courier:BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePublicId { get; set; }

        public Nullable<int> ActiveOrderId { get; set; }
        public bool IsActive { get; set; }
        public double Balance { get; set; }
        public double TotalProfit { get; set; }
        public int TotalOrder { get; set; }

        public string AppUserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
