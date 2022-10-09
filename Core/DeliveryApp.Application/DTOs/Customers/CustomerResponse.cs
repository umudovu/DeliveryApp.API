using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.DTOs.Customers
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePublicId { get; set; }


        public string AppUserId { get; set; }
    }
}
