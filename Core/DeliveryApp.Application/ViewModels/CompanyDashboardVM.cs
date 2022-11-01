using DeliveryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.ViewModels
{
    public class CompanyDashboardVM
    {
        public Company Company { get; set; }
        public List<Product> BestSeller { get; set; }

    }
}
