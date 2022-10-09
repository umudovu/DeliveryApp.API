using DeliveryApp.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.DTOs
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Adress { get; set; }
		public string? PhoneNumber { get; set; }
		public string? StartJob { get; set; }
		public string? EndJob { get; set; }
		public string? ImageUrl { get; set; }

        public List<CategoryResponse>? Categories { get; set; }
    }
}
