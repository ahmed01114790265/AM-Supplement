using AMSupplement.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AM_Supplement.Dashboard.Controllers
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            // Using a scope ensures services are disposed of correctly
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // ================== CREATE ROLES ==================
            string[] roles = new[] { "Admin", "User" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // This will now work because we fixed the ApplicationRole constructor
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                    Console.WriteLine($"Role {roleName} created.");
                }
            }

            // ================== CREATE DEFAULT ADMIN ==================
            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Admin user created and assigned to Admin role.");
                }
                else
                {
                    Console.WriteLine("Failed to create Admin user:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"- {error.Description}");
                }
            }
            else
            {
                // Ensure existing admin is actually in the role
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}