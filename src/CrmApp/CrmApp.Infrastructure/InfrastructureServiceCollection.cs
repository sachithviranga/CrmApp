using CrmApp.Application.Interfaces;
using CrmApp.Infrastructure.Data;
using CrmApp.Infrastructure.Mapping;
using CrmApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace CrmApp.Infrastructure
{
    /// <summary>
    /// Dependency injection registrations for the Infrastructure layer.
    /// Configures the database context, repositories, and infrastructure mapping profiles.
    /// </summary>
    public static class InfrastructureServiceCollection
    {
        /// <summary>
        /// Registers Infrastructure layer services and configuration.
        /// </summary>
        /// <param name="services">The DI service collection.</param>
        /// <param name="configuration">Application configuration.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Db Context
            services.AddDbContext<CrmDbContext>(it =>
            {
                it.UseSqlServer(configuration["Database:ConnectionString"]);
            }, ServiceLifetime.Scoped);

            #endregion


            #region AutoMapper
            services.AddAutoMapper(cfg => cfg.AddProfile<EntityProfile>());
            #endregion


            #region Repositories

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion

            return services;
        }
    }
}
