using AM_Sopplement.DataAccess;
using AM_Supplement.Application.Services;
using AM_Supplement.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM_Supplement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddScoped<IProductService, ProductService>();
            return services;
        }

    }
}
