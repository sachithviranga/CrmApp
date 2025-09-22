using CrmApp.Application;
using CrmApp.Infrastructure;

namespace CrmApp.Server.Extensions
{
    /// <summary>
    /// API host layer service registration aggregator.
    /// Wires Infrastructure first and then Application services into the container.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Registers all application services required by the API host.
        /// </summary>
        /// <param name="services">The DI service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            InfrastructureServiceCollection.RegisterServices(services, configuration);

            ApplicationServiceCollection.RegisterServices(services, configuration);

            return services;
        }
    }
}
