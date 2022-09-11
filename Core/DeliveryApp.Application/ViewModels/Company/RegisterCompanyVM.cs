using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.ViewModels.Company
{
    public class RegisterCompanyVM
    {
		public string Email { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string StartJob { get; set; }
        public string EndJob { get; set; }
		public IFormFile Photo { get; set; }
		public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
