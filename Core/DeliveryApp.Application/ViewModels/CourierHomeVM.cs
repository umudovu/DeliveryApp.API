using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.ViewModels
{
    public class CourierHomeVM
    {
        public Courier Courier { get; set; }
        public List<Order> Orders { get; set; }
    }
}
