using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Transversal.EmailService;
using Transversal.EmailService.Configurations;
using Transversal.EmailService.Factory;
using Transversal.Helpers;

namespace Core.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly UserManager<Users> _userManager;
        private readonly EmailSendGridConfiguration _emailSendGridConfiguration;
        private readonly IGenericEmailFactory _genericEmailFactory;
        private readonly FrontConfiguration _frontConfiguration;

        public EmailService(
            ILogger<EmailService> logger,
            UserManager<Users> userManager,
            EmailSendGridConfiguration emailSendGridConfiguration,
            IGenericEmailFactory GenericEmailFactory,
            FrontConfiguration frontConfiguration
            )
        {
            _logger = logger;
            _userManager = userManager;
            _emailSendGridConfiguration = emailSendGridConfiguration;
            _genericEmailFactory = GenericEmailFactory;
            _frontConfiguration = frontConfiguration;
        }

        public async Task RegistrationEmailAsync(Users user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = $"{_frontConfiguration.ConfirmAccountPage}?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            await SendEmailRegisterUser(user, url);
        }

        public async Task ForgotPasswordSendEmail(Users user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_frontConfiguration.RecoverPasswordPage}?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";

            var message = new Message
                (
                    to: new string[] { user.Email },
                    subject: "Restauración de contraseña",
                    content: "Hola. Restaure su contraseña",
                    emailFrom: _emailSendGridConfiguration.From,
                    attachments: null
                );
            
            string body = HtmlDocumentHelper.GetHtmlDocument("ForgotPassword.html", null, new List<string> {callbackUrl});
            message.Content = body;
            await SendEmailAsync(message);

        }




        #region Helpers
        private async Task SendEmailRegisterUser(Users user, string url)
        {
            var message = new Message
                (
                    to: new string[] { user.Email },
                    subject: "Registro de usuario",
                    content: "Hola. Acepte registrarte",
                    emailFrom: _emailSendGridConfiguration.From,
                    attachments: null
                );

            string body = HtmlDocumentHelper.GetHtmlDocument("Register.html", null, new List<string> { url });
            message.Content = body;
            await SendEmailAsync(message);
        }
        
        private async Task<string> SendEmailAsync(Message message, IFormFileCollection files = null)
        {
            var EmailService = _genericEmailFactory.GetDefault();
            var result = await EmailService.SendEmailAsync(message);
            return result.Data;
        }
        #endregion
    }
}
