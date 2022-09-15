using DeliveryApp.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace DeliveryApp.Infrastructure
{
    static class Configuration
    {
        public static string GetSecretKey
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/DeliveryApp.API"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetSection("TokenKey").Value;
            }
        }


    }
}
