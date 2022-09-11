﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Domain.Entities.Photo
{
	public class Photo
	{
		public int Id { get; set; }
		public string? Url { get; set; }
		public string? PublicId { get; set; }
	}
}
