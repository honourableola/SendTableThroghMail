using EmailTester.Models;
using EmailTester.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTester.Services
{
    public class MailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        List<User> users = new List<User>()
        {
            new User(1, "AmotulFareed", "Oyekunle"),
            new User(2, "Micheal", "Jordan"),
            new User(3, "Seyi", "Law"),
            new User(4, "Toheeb", "Imran"),
            new User(5, "Frank", "Tom")
        };
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            StringBuilder builder = new StringBuilder();
           
            builder.Append("<p style='color: #000000; background-color: #ffffff'>Below are the Users registered on Unique Todo System. <br> </ p>");
            builder.Append("<table border=1 ><tr>");          

            builder.Append("<table border=1 ><tr>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "Id" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "First Name" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "Last Name" + "</th>");
            builder.Append("</tr>");

                foreach (var data in users)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>" + data.Id + "</td>");
                    builder.Append("<td>" + data.FirstName + "</td>");
                    builder.Append("<td>" + data.LastName + "</td>");
                    builder.Append("</tr>");
                }

                builder.Append("</table>");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

             var mainBuilder = new BodyBuilder();
            mainBuilder.HtmlBody = builder.ToString();
            email.Body = mainBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
