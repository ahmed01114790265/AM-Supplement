using AM_Supplement.Application;
using AM_Supplement.Contracts.Services;
using AM_Supplement.Dashboard.Controllers;
using AM_Supplement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Net;

namespace AM_Supplement.Dashboard
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            //register services
            // في ملف Program.cs
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.LogoutPath = "/account/logout";
                x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                x.Cookie.SameSite = SameSiteMode.Lax; // أو None لو HTTPS شغال تمام
                x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("User", p => p.RequireAuthenticatedUser().RequireRole("User"));
                x.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                x.AddPolicy("Supervisor", p => p.RequireAuthenticatedUser().RequireRole("Supervisor"));
                // x.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            builder.Services.AddControllersWithViews()
                            .AddViewLocalization()
                            .AddDataAnnotationsLocalization();

            ///localization
            //register localization services 
            // use the below line for registering localization services if the 
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            //use this in case localized resources files in different project
            builder.Services.AddLocalization();
            builder.Services.AddSingleton(static serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
               var type = typeof(AM_Supplement.Shared.Localization.SharedResource);
               return factory.Create(type);
            });

            var supportedCultures = new[] { "en", "ar", "fr" };

            //Configure the application to detect and apply the culture for each request.
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("en")
                       .AddSupportedCultures(supportedCultures)
                       .AddSupportedUICultures(supportedCultures);

                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AMSublementDbContext>();

                    // الخطوة دي هتحل مشكلة Invalid column name 'ActivatedAsAMember'
                    await context.Database.MigrateAsync();

                    // استدعاء ميثود الـ Seed اللي إنت بعتها
                    // تأكد من اسم الكلاس اللي جواه الميثود (هفترض اسمه DbSeeder)
                    await DataSeeder.SeedRolesAndAdmin(services);

                    Console.WriteLine("Database Migrated & Seeded Successfully!");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }

            // 5. إعداد الملفات الثابتة (Static Files)
            app.UseStaticFiles(); // الافتراضي لـ wwwroot
           
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            // Apply localization setting to every HTTP request
           var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}"
                   );
            });
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // استدعاء الميثود اللي انت كتبتها لتجهيز الـ Roles والـ Admin
                    await DataSeeder.SeedRolesAndAdmin(services);
                    Console.WriteLine("Data Seeding completed successfully.");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during database seeding.");
                }
            }
            app.MapControllers();
            app.Run();
        }
    } 
    }
