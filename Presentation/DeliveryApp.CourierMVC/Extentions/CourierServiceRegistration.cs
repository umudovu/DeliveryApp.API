using DeliveryApp.Infrastructure;
using DeliveryApp.Persistence;

namespace DeliveryApp.CourierMVC.Extentions
{
    public static class CourierServiceRegistration
    {
        public static void AddCourierServiceRegistration(this IServiceCollection services, IConfiguration config)
        {
            services.AddPersistenceServices(config);
            services.AddInfrastructureServices();
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromDays(22);
            });



        }
    }
}
