using CrmApp.Infrastructure;

namespace CrmApp.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            InfrastructureServiceCollection.RegisterServices(services, configuration);


            return services;
        }
    }
}
