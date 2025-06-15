using AM_Supplement.Application;
using AM_Supplement.Contracts.Services;

namespace AM_Supplement.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        
            //register services
            builder.Services.AddApplication(builder.Configuration);
           
            builder.Services.AddRazorComponents()
             .AddInteractiveServerComponents();
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();
            // test service

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService(typeof(IProductService));
            }
            if(app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/PrroductController/error");
            }
            app.UseStaticFiles();
            app.UseRouting();
           
            app.MapControllerRoute(name: "defualt", pattern: "{controller=PrroductController}/{action=Index}");
            
            app.Run();
        }
    }
}
