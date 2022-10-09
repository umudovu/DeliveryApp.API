using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.DTOs.Category
{
	public class CategoryResponse
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string ImageUrl { get; set; }
        public int CompanyId { get; set; }
        public List<ProductResponse>? Products { get; set; }
    }
}
