using BookLending.Application.Interface;
using BookLending.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace BookLending.Infrastructure.Services
{
    public class JwtService : IJwtTokenGenerator
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly JwtSettings jwtSettings;
        public JwtService(IOptionsSnapshot<JwtSettings> jwt, UserManager<IdentityUser> _userManager) 
        {
            jwtSettings = jwt.Value;
            userManager = _userManager;

        }
        public async Task<string> GenerateToken(IdentityUser User)
        {

            var role =await userManager.GetRolesAsync(User);
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecritKey));
           SigningCredentials signingCred = new SigningCredentials
                                        (signInKey, SecurityAlgorithms.HmacSha256);



            //add data in token
            List<Claim> UserClaims = new List<Claim>();
            UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, User.Id));
            UserClaims.Add(new Claim(ClaimTypes.Name, User.UserName));

            //create token
            var token = new JwtSecurityToken(
           issuer: jwtSettings.IssuerIP,
           audience: jwtSettings.AudienceIP,
           claims: UserClaims,
           expires: DateTime.Now.AddHours(1),
           signingCredentials: signingCred
       );

            return new JwtSecurityTokenHandler().WriteToken(token);

           
        }
    }
}
