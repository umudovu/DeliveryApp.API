using DeliveryApp.Application.Abstractions.Token;
using DeliveryApp.Infrastructure.Mapping;
using DeliveryApp.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Infrastructure
{
	public static class ServiceRegistration
	{
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddAutoMapper(opt =>
            {
                opt.AddProfile(new MappingProfile());
            });

        }
    }
}
