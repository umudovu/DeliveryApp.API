using DeliveryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string? Content { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
