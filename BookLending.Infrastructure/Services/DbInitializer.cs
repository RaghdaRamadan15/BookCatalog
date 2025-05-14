using BookLending.Domain.Enums;
using BookLending.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookLending.Infrastructure.Services
{
    public static class DbInitializer
    {


        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            
            //string[] roleNames = { "Admin", "Member" };
            var roleNames = Enum.GetValues(typeof(Roles))
                        .Cast<Roles>()  
                        .Select(role => role.ToString())  
                        .ToList();

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
            var user = await userManager.FindByEmailAsync(SettingAdmin.Email);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = SettingAdmin.UserName,
                    Email = SettingAdmin.Email,
                };

                
                var result = await userManager.CreateAsync(user, SettingAdmin.Password);

                if (result.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(user, SettingAdmin.Role);
                }
                
            }
        }
    }
        
    }
