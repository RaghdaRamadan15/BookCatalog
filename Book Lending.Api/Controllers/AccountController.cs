using BookLending.Application.Dtos;
using BookLending.Application.Services;
using BookLending.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Book_Lending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController( AuthService _authService) : ControllerBase
    {
       

        [HttpPost("register")]
       
        public async Task<IActionResult> Register(RegisterUser user)
        {

            if (ModelState.IsValid)
            {
                var result= await _authService.RegisterAsync(user);
                return Ok(result);
            }
            return BadRequest("enter data");
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login(LoginUser user)
        {
            var result = await _authService.LoginAuthAsync(user);
            if (result==null)
                return Unauthorized("Invalid credentials");

            return Ok(new { token = result.Token,ex=result.Expiration });
        }
    }
}
