using BookLending.Application.Dtos;
using BookLending.Application.Interface;
using BookLending.Application.Services;
using BookLending.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Testing.BoohLending.AppTesting
{
    public class AuthServiceTesting
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IJwtTokenGenerator> jwtTokenGeneratorMock;
        private readonly AuthService authService;
        public AuthServiceTesting()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            authService = new AuthService(userRepositoryMock.Object, jwtTokenGeneratorMock.Object);
        }

        #region Create Created User Successfully
        [Fact]
        public async Task RegisterAsyncCreatedUserSuccessfully()
        {
            
            var registerDto = new RegisterUser
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!"
            };

            userRepositoryMock.Setup(r => r.CreateUserAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await authService.RegisterAsync(registerDto);
            Assert.Single(result);
            Assert.Equal("Created", result[0]);
        }
        #endregion
        #region Test login User Successfully
        [Fact]
        public async Task LoginAsyncUserSuccessfully()
        {
            var loginDto = new LoginUser
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
            var returnUser = new IdentityUser
            {
                Email = loginDto.Email
            };
            userRepositoryMock.Setup(x=>x.GetByEmailAsync(loginDto.Email)).ReturnsAsync(returnUser);

            userRepositoryMock.Setup(x => x.CheckPasswordAsync(returnUser, loginDto.Password)).ReturnsAsync(true);
             jwtTokenGeneratorMock.Setup(t => t.GenerateToken(returnUser))
              .ReturnsAsync("mocked-jwt-token");
            // Act
            var result = await authService.LoginAuthAsync(loginDto);
            Assert.NotNull(result);
            Assert.Equal("mocked-jwt-token", result.Token);

        }
        #endregion
    }
}
