using AM_Supplement.Application;
using AM_Supplement.Shared.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace AM_Supplement.Dashboard
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
                x.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                // x.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            

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

            // Add services to the container.
            builder.Services.AddControllersWithViews()
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization(options =>
               {
                   // Ensure Data Annotations use SharedResource from external layer
                   var type = typeof(SharedResource);
                   options.DataAnnotationLocalizerProvider = (modelType, factory) =>
                       factory.Create(type);
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

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Index}/{id?}");

            app.Run();


            app.UseAuthentication();
            app.UseAuthorization();
            // appy localization setting to every http request
            var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.MapControllers();
            app.Run();
        }
    }
}
