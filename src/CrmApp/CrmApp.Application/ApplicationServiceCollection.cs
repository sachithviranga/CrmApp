using CrmApp.Application.Interfaces;
using CrmApp.Application.Mapping;
using CrmApp.Application.Services;
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

            return services;
        }
    }
}
