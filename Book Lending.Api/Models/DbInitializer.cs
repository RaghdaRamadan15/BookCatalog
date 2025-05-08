using Microsoft.AspNetCore.Identity;

namespace Book_Lending.Api.Models
{
    public static class DbInitializer
    {


        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {

            string[] roleNames = { "Admin", "Member" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {

                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }



        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "admin11",
                    Email = "admin@gmail.com"
                };

                
                var result = await userManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                
            }
        }
    }
        
    }
