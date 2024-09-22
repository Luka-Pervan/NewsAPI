using Microsoft.AspNetCore.Identity;
using NewsAPI.Models;

namespace NewsAPI.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roleNames = { "Admin", "Author", "Reader" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole<int>(roleName));
                }
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<User> userManager)
        {
            // Check if the Admin user already exists
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    FirstName = "AdminFirstName", 
                    LastName = "AdminLastName",  
                    Role = "Admin",
                    EmailConfirmed = true,
                };

                // Create the Admin user
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    // Assign Admin role to the created user
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
