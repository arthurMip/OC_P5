using Microsoft.AspNetCore.Identity;

namespace ExpressVoitures.Helpers;

public static class RoleInitializer
{
    public static async Task CreateRolesAndAdminUser(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        // Define the roles to be created
        string[] roleNames = ["Admin", "User"];

        foreach (var roleName in roleNames)
        {
            // Check if the role exists, if not create it
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create an Admin user if it doesn't exist
        const string adminEmail = "admin@voiture-express.fr";
        IdentityUser? adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            var admin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail
            };
            var createAdmin = await userManager.CreateAsync(admin, "AdminPassword123!");

            if (createAdmin.Succeeded)
            {
                // Assign the Admin role to the user
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
