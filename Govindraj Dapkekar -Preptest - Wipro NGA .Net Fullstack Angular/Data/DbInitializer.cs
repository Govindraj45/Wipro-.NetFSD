using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PrepTestMilestone3.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure the SQLite database is created and schema is applied
            context.Database.EnsureCreated();

            // Seed Roles if they don't exist
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var createPowerUser = await userManager.CreateAsync(user, "Admin@123");
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            // Seed Regular User
            var regularUser = await userManager.FindByNameAsync("user1");
            if (regularUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    EmailConfirmed = true
                };

                var createRegularUser = await userManager.CreateAsync(user, "User@123");
                if (createRegularUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
