using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }
        public Nullable<DateTime> RemovedDate { get; set; }
    }
}
