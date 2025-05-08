using BookLending.Application.Dtos;
using BookLending.Application.Interface;
using BookLending.Infrastructure.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;
using Microsoft.EntityFrameworkCore;


namespace BookLending.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting emailSetting;
        private readonly BookContext bookContext;
        public EmailService(IOptions<EmailSetting> _emailSetting, BookContext _bookContext)
        {
            emailSetting = _emailSetting.Value;
            bookContext = _bookContext;
        }


        public async Task<IEnumerable<SendingEmail>> GetMemberReturnAsyn()
        {
            List <SendingEmail> result = new List<SendingEmail>();
            var value = await bookContext.borrows.Where(x => !x.IsReturned && x.DueDate < DateTime.Today).Select(x => new SendingEmail
            {
                MemberName=x.User.UserName,
                BookName=x.Book.Name,
                MemberEmail=x.User.Email
            }).ToListAsync();
            if (value != null) 
            {
                result.AddRange(value);
                return result;
            }
            return null;


        }
        //IEnumerable<SendingEmail> sendingEmails
        public async Task SendEmailReturnAsync()
        {
            var sendingEmails= await GetMemberReturnAsyn();
            var smtpClient = new SmtpClient(emailSetting.Host)
            {
                Port = int.Parse(emailSetting.Port),
                Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password),
                EnableSsl = true,
            };
            foreach(SendingEmail sendingEmail in sendingEmails)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailSetting.From),
                    Subject = "Not return book",

                    Body = $"please {sendingEmail.MemberName} hurry up bring back the books{sendingEmail.BookName} period available has expired ",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(sendingEmail.MemberEmail);

                await smtpClient.SendMailAsync(mailMessage);

            }
            
        }
    }
}
