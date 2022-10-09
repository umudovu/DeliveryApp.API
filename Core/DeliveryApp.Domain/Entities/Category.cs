using DeliveryApp.Domain.Entities.Common;
using DeliveryApp.Domain.Entities.Photo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
	public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImagePublicId { get; set; }

        public ICollection<Category>? Children { get; set; }
        public Category? Parent { get; set; }

        public ICollection<Product>? Products { get; set; }

		public int CompanyId { get; set; }
        public Company? Company { get; set; }



	}
}
