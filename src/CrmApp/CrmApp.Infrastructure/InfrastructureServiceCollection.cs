using CrmApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
