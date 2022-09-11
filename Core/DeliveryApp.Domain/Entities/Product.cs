using DeliveryApp.Domain.Entities.Common;
using DeliveryApp.Domain.Entities.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public double Price { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImagePublicId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
