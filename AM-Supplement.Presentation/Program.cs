using AM_Supplement.Application;
using AM_Supplement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
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
                x.LogoutPath = "/account/login";
                x.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("User",p => p.RequireAuthenticatedUser().RequireRole("User"));
                x.AddPolicy("Admin", p=> p.RequireAuthenticatedUser().RequireRole("Admin"));
                x.AddPolicy("SuperVisor", p=> p.RequireAuthenticatedUser().RequireRole("SuperVisor"));
                x.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();  
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
            app.MapControllers();
            app.Run();
        }
    }
}
