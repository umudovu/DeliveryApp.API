﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Infrastructure.Helpers
{
	public class CloudinarySettings
	{
		public string? settingName = "CloudinarySettings";
		public string? CloudName { get; set; }
		public string? ApiKey { get; set; }
		public string? ApiSecret { get; set; }
	}
}
