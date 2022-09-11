using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities.Photo
{
	public class ProductPhoto:Photo
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }
	}
}
