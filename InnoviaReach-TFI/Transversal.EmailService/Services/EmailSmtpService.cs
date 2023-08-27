using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Transversal.EmailService.Configurations;
using Transversal.EmailService.Contracts;
using Transversal.Helpers.ResultClasses;
using MailKit.Security;
using System.IO;
using MailKit.Net.Smtp;

namespace Transversal.EmailService.Services
{
    public class EmailSmtpService : GenericEmailService, IEmailSmtpService
    {
        private readonly EmailSMTPConfiguration _emailConfig;
        private readonly ILogger<EmailSmtpService> _logger;

        public EmailSmtpService(EmailSMTPConfiguration emailConfig, ILogger<EmailSmtpService> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }

        public override void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }
        
        public override async Task<IGenericResult<string>> SendEmailAsync(Message message)
        {
            GenericResult<string> genericResult = new GenericResult<string>();
            var mailMessage = CreateEmailMessage(message);
            var result = await SendAsync(mailMessage);
            genericResult.Data = result;
            return genericResult;
        }
        
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.DisplayName, message.EmailFrom));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Priority = MessagePriority.Urgent;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

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

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An exception has occurred.");
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task<string> SendAsync(MimeMessage mailMessage)
        {
            try
            {
                using var client = new SmtpClient();

                if (_emailConfig.UseSsl)
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                else
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);

                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                await client.SendAsync(mailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has occurred, Exception Message: " + ex.Message);
            }

            return Guid.NewGuid().ToString();
        }
    }
}
