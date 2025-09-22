using CrmApp.Application.Interfaces;
using CrmApp.Application.Mapping;
using CrmApp.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrmApp.Application
{
    /// <summary>
    /// Dependency injection registrations for the Application layer.
    /// Adds application services and mapping profiles to the service container.
    /// </summary>
    public static class ApplicationServiceCollection
    {
        /// <summary>
        /// Registers Application layer services and configuration.
        /// </summary>
        /// <param name="services">The DI service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper

            services.AddAutoMapper(cfg => cfg.AddProfile<ApplicationMappingProfile>());

            #endregion

            #region Services

            services.AddScoped<ICustomerService, CustomerService>();

            #endregion

            return services;
        }
    }
}
