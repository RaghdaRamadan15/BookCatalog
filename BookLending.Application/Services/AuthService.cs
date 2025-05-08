using Azure.Core;
using BookLending.Application.Dtos;
using BookLending.Application.Interface;
using BookLending.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository userRepository;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthService(IUserRepository _userRepository, IJwtTokenGenerator _jwtTokenGenerator)
        {
            userRepository = _userRepository;
            jwtTokenGenerator = _jwtTokenGenerator;
        }

        public async Task<string> RegisterAsync(RegisterUser User)
        {
            var member = new IdentityUser
            {
                UserName = User.Username,
                Email = User.Email
            };

            var Ruselt = await userRepository.CreateUserAsync(member, User.Password);
            if (Ruselt.Succeeded)
            {
                return "Created";
            }
            return  "NotCreated";

        }
        public async Task<AuthResult> LoginAuthAsync(LoginUser User)
        {
            AuthResult authResult = new AuthResult();
            var returnUser=await userRepository.GetByEmailAsync(User.Email);
            var validPassword = await userRepository.CheckPasswordAsync(returnUser, User.Password);
            if (returnUser == null || !validPassword)
            {
                
                return authResult= null;
            }
            var token =  await jwtTokenGenerator.GenerateToken(returnUser);

            authResult.Token= token;
            authResult.Expiration= DateTime.UtcNow.AddDays(1);
             
            return authResult;
        }

       
    }
}
