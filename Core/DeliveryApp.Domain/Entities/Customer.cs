using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Address { get; set; }

        public string? ImageUrl { get; set; } = "https://res.cloudinary.com/webapi/image/upload/v1666979522/profileunnamed_lskq8l.png";
        public string? ImagePublicId { get; set; } = "profileunnamed_lskq8l";


        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<Comment>? Comments { get; set; }


    }
}
