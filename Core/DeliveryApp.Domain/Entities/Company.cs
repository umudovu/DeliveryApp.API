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
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? PhoneNumber { get; set; }
		public string? StartJob { get; set; }
		public string? EndJob { get; set; }
		public double Balance { get; set; }

        public string? Address { get; set; }
        public string? LatCoord { get; set; }
		public string? LngCoord { get; set; }

		public double ServiceFee { get; set; }
        public string? ServiceTime { get; set; }

        public string? ImageUrl { get; set; }
		public string? ImagePublicId { get; set; }

		public string? AppUserId { get; set; }
		public AppUser? User { get; set; }

		public ICollection<Product>? Products { get; set; }
		public ICollection<Category>? Categories { get; set; }
		public ICollection<Order>? Orders { get; set; }
		public ICollection<Comment>? Comments { get; set; }

	}
}
