using CrmApp.Application;
using CrmApp.Infrastructure;
using FluentValidation.AspNetCore;

namespace CrmApp.Server.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddFluentValidationAutoValidation();

            InfrastructureServiceCollection.RegisterServices(services, configuration);

            ApplicationServiceCollection.RegisterServices(services, configuration);

            return services;
        }
    }
}
