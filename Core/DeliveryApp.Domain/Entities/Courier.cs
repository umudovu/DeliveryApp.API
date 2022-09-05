using DeliveryApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities
{
    public class Courier:BaseEntity
    {
        public bool IsActive { get; set; }
        public double Balance { get; set; }

        public string AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
