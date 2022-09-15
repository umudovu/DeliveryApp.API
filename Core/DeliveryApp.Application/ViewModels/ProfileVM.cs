using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.ViewModels
{
    public class ProfileVM
    {
        public AppUser? User { get; set; }
        public Company? Company { get; set; }
        public IFormFile Photo { get; set; }
    }
}
