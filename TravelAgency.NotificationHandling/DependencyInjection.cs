using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Repository_Contracts;
using TravelAgency.Infrastructure.Persistence.Repositories;

namespace TravelAgency.NotificationHandling
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNotificationHandlingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register configuration if needed by your repository
            services.AddSingleton(configuration);

            // Register your repositories and other services
            services.AddSingleton<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}
