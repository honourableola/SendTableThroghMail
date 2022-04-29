using EmailTester.Models;
using EmailTester.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailTester.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest mailRequest)
        {
            /*try
            {*/
                await _emailService.SendEmailAsync(mailRequest);
                return Ok();
            /*}
            catch (Exception ex)
            {

                throw;
            }*/
        }
    }
}
