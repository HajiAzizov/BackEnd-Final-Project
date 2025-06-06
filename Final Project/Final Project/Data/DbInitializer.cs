using Microsoft.AspNetCore.Identity;

namespace Final_Project.Data
{
    public class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Rollar
            string[] roles = new string[] { "SuperAdmin", "Admin", "Member" };

            foreach (var role in roles)
            {
                // Əgər rol yoxdursa yaradılır
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
