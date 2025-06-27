using AMSupplement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AMSupplement.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSuplementDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AMSublementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("cs"), b => b.MigrationsAssembly("AMSupplement.Domain")));
            service.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AMSublementDbContext>
                ();
               
            return service;
        }

    }
}
