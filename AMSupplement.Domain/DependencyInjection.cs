using AM_Supplement.Infrastructure.Persistence;
using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AMSupplement.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSuplementDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AMSublementDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AMSupplement.Domain")));
            service.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // إيقاف شرط البريد الإلكتروني
                options.User.RequireUniqueEmail = false;

                // إعدادات كلمة المرور (يمكنك تبسيطها لتسهيل الدخول)
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<AMSublementDbContext>
                ().AddDefaultTokenProviders();
               
            return service;
        }

    }
}
