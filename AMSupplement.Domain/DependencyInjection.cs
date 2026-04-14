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
            service.AddDbContext<AMSublementDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    // تحديد مكان الـ Migrations
                    sqlOptions.MigrationsAssembly("AMSupplement.Domain");

                    // تفعيل خاصية إعادة المحاولة عند حدوث أعطال مؤقتة في الاتصال
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));

            service.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // إيقاف شرط البريد الإلكتروني
                options.User.RequireUniqueEmail = false;

                // إعدادات كلمة المرور
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<AMSublementDbContext>()
            .AddDefaultTokenProviders();

            return service;
        }

    }
}
