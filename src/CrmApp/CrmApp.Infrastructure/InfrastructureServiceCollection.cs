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
    public static class InfrastructureServiceCollection
    {
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
