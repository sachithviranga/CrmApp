using CrmApp.Application;
using CrmApp.Application.Validators;
using CrmApp.Infrastructure;
using CrmApp.Server.Middleware;
using CrmApp.Shared.DTO;
using FluentValidation;
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



            services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();

            services.AddValidatorsFromAssemblyContaining<UpdateCustomerRequestValidator>();

            return services;
        }
    }
}
