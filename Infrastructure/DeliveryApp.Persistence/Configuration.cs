using DeliveryApp.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Persistence
{
    static class Configuration
    {
        public static  string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DeliveryApp.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("MsSql");
            }
        }

        public static CloudinarySettings GetCloudinarySettings
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DeliveryApp.API"));
                configurationManager.AddJsonFile("appsettings.json");
                CloudinarySettings cloudinary = new()
                {
                    CloudName = configurationManager.GetSection("CloudinarySettings:CloudName").Value,
                    ApiKey = configurationManager.GetSection("CloudinarySettings:ApiKey").Value,
                    ApiSecret = configurationManager.GetSection("CloudinarySettings:ApiSecret").Value,

                };
                return cloudinary;
            }
        }



    }
}
