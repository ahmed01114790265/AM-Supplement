using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace AMSupplement.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSuplementDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AMSublementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("cs"), b => b.MigrationsAssembly("AMSupplement.Domain")));
            return service;
        }

    }
}
