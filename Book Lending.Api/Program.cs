using Book_Lending.Api.Models;
using BookLending.Application.Interface;
using BookLending.Application.MapSter;
using BookLending.Application.Services;
using BookLending.Domain.Interfaces;
using BookLending.Infrastructure.Models;
using BookLending.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using BookContext = BookLending.Infrastructure.Models.BookContext;

namespace Book_Lending.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //DI for mapster
            MapBook.RegisterMappings();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //#region Swagger Setting
            //builder.Services.AddSwaggerGen(swagger =>
            //{
            //    //This is to generate the Default UI of Swagger Documentation    
            //    swagger.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Version = "v1",
            //        Title = "ASP.NET 8 Web API",
            //        Description = "  Projrcy"
            //    });
            //    // To Enable authorization using Swagger (JWT)    
            //    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.ApiKey,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            //    });
            //    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //        new OpenApiSecurityScheme
            //        {
            //        Reference = new OpenApiReference
            //        {
            //        Type = ReferenceType.SecurityScheme,
            //        Id = "Bearer"
            //        }
            //        },
            //        new string[] {}
            //        }
            //        });
            //});
            //#endregion


            builder.Services.AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));

            });
            //-------------------------------------------------
            //Injaction  IdentityUser
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookContext>();

            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<BookServices>();
            builder.Services.AddScoped<BorrowingServce>();
            builder.Services.AddScoped<IUserBorrowingRepository, UserBorrowingRepository>();


            // injaction to send email
            //sendemailfor register
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Option setting 
            builder.Services.Configure<JwtSettings>(
              builder.Configuration.GetSection("JWT"));

            //option emailsetting
            builder.Services.Configure<EmailSetting>(
             builder.Configuration.GetSection("Smtp"));


            //token
            builder.Services.AddAuthentication(options =>
            {
                //Check JWT Token Header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //[authrize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//unauth
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });


            // Setting for Hangfire
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Database")));
            builder.Services.AddHangfireServer();

            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Hangfire
            app.UseHangfireDashboard();

            app.MapControllers();

            


            // scope to add Seed Data in database 
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                // Add roles
                await DbInitializer.SeedRolesAsync(roleManager);

                // Add users
                await DbInitializer.SeedUsersAsync(userManager);

                //Run Job
                RecurringJob.AddOrUpdate<EmailService>(
                job => job.SendEmailReturnAsync(),
                Cron.Daily);
                
            }

            app.Run();
        }
}
}
