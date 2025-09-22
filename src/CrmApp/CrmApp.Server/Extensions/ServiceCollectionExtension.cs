using CrmApp.Application;
using CrmApp.Infrastructure;

namespace CrmApp.Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            InfrastructureServiceCollection.RegisterServices(services, configuration);

            ApplicationServiceCollection.RegisterServices(services, configuration);

            return services;
        }
    }
}
