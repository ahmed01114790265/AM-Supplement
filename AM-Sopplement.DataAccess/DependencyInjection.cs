using AM_Sopplement.DataAccess.Repositories.Implementation;
using AM_Sopplement.DataAccess.Repositories.Interfaces;
using AM_Sopplement.DataAccess.UnitOfWork.Interfaces;
using AMSupplement.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AM_Sopplement.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSuplementDbContext(configuration);
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IUnitOfWork, UnitOfWork.Implementation.UnitOfWork>();
            return service;
        }
    }
}
