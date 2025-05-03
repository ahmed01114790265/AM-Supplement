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
            var app = builder.Build();

            // test service

            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService(typeof(IProductService));
            }
            app.MapGet("/", () => "Hello World!");
            app.Run();
        }
    }
}
