using AM_Supplement.Application;
using AM_Supplement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Net;

namespace AM_Supplement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //register services
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.LogoutPath = "/account/logout";
                x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("User", p => p.RequireAuthenticatedUser().RequireRole("User"));
                x.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                x.AddPolicy("Supervisor", p => p.RequireAuthenticatedUser().RequireRole("Supervisor"));
               // x.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            builder.Services.AddControllersWithViews();

            ///localization
            //register localization services 
            // use the below line for registering localization services if the 
            //builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

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

            // test service

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService(typeof(IProductService));
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            // appy localization setting to every http request
            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=ChangePassword}"
                   );
            });
            app.MapControllers();
            app.Run();
        }
    }
}
