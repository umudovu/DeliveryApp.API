using DeliveryApp.Infrastructure;
using DeliveryApp.Persistence;

namespace DeliveryApp.API.Extentions
{
    public static class CustomerServiceRegistration
    {
        public static void AddCustomerServiceRegistration(this IServiceCollection services, IConfiguration config)
        {
            services.AddPersistenceServices(config);
            services.AddInfrastructureServices();
            



        }
    }
}
