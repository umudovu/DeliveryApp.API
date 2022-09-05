using DeliveryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string DeliveryDate { get; set; }
        public double Balance { get; set; }

        public string AppUserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
