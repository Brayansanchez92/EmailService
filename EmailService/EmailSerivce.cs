using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Sabio.Data.Providers;
using Sabio.Models.AppSettings;
using Sabio.Models.Requests.EmailRequest;
using Sabio.Services.Interfaces.Email;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services.Security.EmailService
{
    public class EmailServiceTest :  IMailService
    {
        private AppKeys _appKeys = null;
        private IWebHostEnvironment _env;
        public EmailServiceTest(IOptions<AppKeys> appkeys,IWebHostEnvironment env)
        {
            _appKeys = appkeys.Value;
            _env = env;
        }

       

        public async Task TestSend(EmailSendRequest email)
        {
             
            SendGridMessage msg = new SendGridMessage()
            {
                From = new EmailAddress(_appKeys.SenderEMail, _appKeys.SenderName),
                Subject = email.Subject,
                PlainTextContent = email.PlainTextContent,
                HtmlContent = GetTemplate(),
                
            };

            msg.AddTo(email.To);
            await SendAsync(msg);
        }


        public string GetTemplate()
        {
            string body = string.Empty;
            string path = _env.WebRootPath + "/EmailTemplates/EmailTemplate.html";
            StreamReader sr = new StreamReader(path);
            {
                body = sr.ReadToEnd();
            }

            body = body.Replace("{&&name}", "Welcome (name goes here)");
            body = body.Replace("{%Welcome}", "Welcome to Chamomile!");
            body = body.Replace("{##Message}", "Send us a message if you have any questions.");
            return body;
        }
           
        private async Task<Response> SendAsync(SendGridMessage msg)
        {
            var client = new SendGridClient(_appKeys.ApiKey);
            return await client.SendEmailAsync(msg);

        }
    }
  
}
