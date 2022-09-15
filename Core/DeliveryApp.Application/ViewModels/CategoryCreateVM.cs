﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.ViewModels
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public IFormFile Photo { get; set; }

    }
}
