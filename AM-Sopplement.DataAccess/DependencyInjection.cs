using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.Repositories.Implementation;
using AMSupplement.Domain;

namespace AM_Sopplement.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSuplementDbContext(configuration);
            service.AddScoped<IProductRepository, ProductRepository>();
            return service;
        }
    }
}
