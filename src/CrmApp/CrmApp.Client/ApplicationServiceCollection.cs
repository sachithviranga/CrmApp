using Microsoft.Extensions.DependencyInjection;

namespace CrmApp.Application
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
