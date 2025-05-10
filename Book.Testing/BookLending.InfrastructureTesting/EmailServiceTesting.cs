using BookLending.Domain.Models;
using BookLending.Infrastructure.Models;
using BookLending.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Testing.BookLending.InfrastructureTesting
{
    public class EmailServiceTesting
    {
        private readonly BookContext context;
       
        private EmailService emailService;
        public EmailServiceTesting()
        {
            var options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: "Email")
            .Options;
            context = new BookContext(options);
             var emailSetting = new Mock<IOptions<EmailSetting>>();

            emailSetting.Setup(x => x.Value).Returns(new EmailSetting
            {
                Host = "smtp.test.com",
                Port = "587",
                Username = "test@example.com",
                Password = "password",
                From = "test@example.com"
            });
           
            emailService = new EmailService(emailSetting.Object, context);
           
            
        }

        [Fact]

        public async Task SendEmail()
        {
            var brorrow = new Borrowing
            {
                UserId = "1",
                BookId=1,
                DueDate = DateTime.Today.AddDays(-1),
                IsReturned = false,
                BorrowDate= DateTime.Today.AddDays(-2)
            };

            await context.borrows.AddAsync(brorrow);


            await context.SaveChangesAsync();
             await emailService.SendEmailReturnAsync();
        }








    }
}
