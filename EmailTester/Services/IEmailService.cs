using EmailTester.Models;
using EmailTester.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTester.Services
{
    public interface IEmailService
    {
        
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
