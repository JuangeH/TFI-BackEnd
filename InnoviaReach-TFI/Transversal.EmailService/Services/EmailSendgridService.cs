using Microsoft.Extensions.Logging;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Transversal.EmailService.Configurations;
using Transversal.EmailService.Contracts;
using Transversal.Helpers.ResultClasses;

namespace Transversal.EmailService.Services
{
    public class EmailSendgridService : GenericEmailService, IEmailSendGridService
    {
        private readonly EmailSendGridConfiguration _emailConfig;
        private readonly ILogger<EmailSendgridService> _logger;
        public EmailSendgridService(EmailSendGridConfiguration emailConfig, ILogger<EmailSendgridService> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }
        
        public override void SendEmail(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            Send(mailMessage.Item1, mailMessage.Item2);
        }

        public override async Task<IGenericResult<string>> SendEmailAsync(Message message)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var mailMessage = CreateEmailMessage(message);
            var result = await SendAsync(mailMessage.Item1, mailMessage.Item2);
            genericResult.Data = result;
            return genericResult;
        }

        private (SendGridMessage, string) CreateEmailMessage(Message message)
        {
            var emailMessage = new SendGridMessage();
            emailMessage.From = new EmailAddress(_emailConfig.From, _emailConfig.DisplayName);
            emailMessage.AddTos(message.To.Select(t => new EmailAddress(t.Address)).ToList());
            emailMessage.Subject = message.Subject;
            emailMessage.HtmlContent = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content);

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    emailMessage.AddAttachment(attachment.FileName, Convert.ToBase64String(fileBytes));
                }
            }

            string sendGridId = Guid.NewGuid().ToString();
            emailMessage.AddCustomArg("message_id", sendGridId);
            //_logger.LogInformation("CreateEmailMessage Succeeded");
            return (emailMessage, sendGridId);
        }

        private void Send(SendGridMessage mailMessage, string sendGridId)
        {
            var client = new SendGridClient(_emailConfig.ApiKey);
            var response = client.SendEmailAsync(mailMessage);
        }

        private async Task<string> SendAsync(SendGridMessage mailMessage, string sendGridId)
        {
            var client = new SendGridClient(_emailConfig.ApiKey);
            var response = await client.SendEmailAsync(mailMessage);

            if (response.IsSuccessStatusCode)
            {
                return sendGridId;
            }

            return null;
        }
    }
}
