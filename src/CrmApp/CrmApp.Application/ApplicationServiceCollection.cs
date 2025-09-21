using CrmApp.Application.Interfaces;
using CrmApp.Application.Mapping;
using CrmApp.Application.Services;
using CrmApp.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrmApp.Application
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AutoMapper

            services.AddAutoMapper(cfg => cfg.AddProfile<ApplicationMappingProfile>());

            #endregion

            #region Services

            services.AddScoped<ICustomerService, CustomerService>();

            #endregion

            #region Validators

            services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCustomerRequestValidator>();

            #endregion

            return services;
        }
    }
}
