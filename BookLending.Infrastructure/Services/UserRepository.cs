using BookLending.Domain.Interfaces;
using BookLending.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> userManager;
        public BookContext bookContext;
        
        public UserRepository(UserManager<IdentityUser> _userManager, BookContext _bookContext)
        {
            userManager = _userManager;
            bookContext = _bookContext;
        }

        public Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
        {
            IdentityResult result= await userManager.CreateAsync(user, password);
            if (result.Succeeded) {
                await userManager.AddToRoleAsync(user, "Member");
                await bookContext.SaveChangesAsync();

            }
            return result;


        }

        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
           var result=  await userManager.FindByEmailAsync(email);
            return result;
        }
    }
}
