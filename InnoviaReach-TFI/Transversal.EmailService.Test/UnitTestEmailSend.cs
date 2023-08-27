using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.EmailService.Configurations;

namespace Transversal.EmailService.Test
{
    //[TestClass]
    //public class UnitTestEmailSend
    //{
    //    private EmailSendgridSenderService _emailSender;

    //    [TestInitialize()]
    //    public void Startup()
    //    {
    //        EmailSendGridConfiguration conf = new EmailSendGridConfiguration();
    //        conf.From = "Gonza_28.00@hotmail.com";
    //        conf.ApiKeyId = "your api key id";
    //        conf.ApiKey = "your api key";
    //        conf.DisplayName = "Gonzalo Ibañez";

    //        //_emailSender = new EmailSendgridSender(conf, new NullLogger<EmailSendgridSender>());
    //        _emailSender = new EmailSendgridSenderService(conf);
    //    }

    //    [TestMethod]
    //    public async Task SendGridEmailAsync()
    //    {
    //        try
    //        {
    //            var apiKey = Environment.GetEnvironmentVariable("your api key");
    //            var client = new SendGridClient(apiKey);
    //            var from = new EmailAddress("test@example.com", "Example User");
    //            var subject = "Sending with SendGrid is Fun";
    //            var to = new EmailAddress("test@example.com", "Example User");
    //            var plainTextContent = "and easy to do anywhere, even with C#";
    //            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
    //            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //            int asd = 1;
    //            int asd2 = 0;
    //            var r = asd / asd2;
    //            var response = await client.SendEmailAsync(msg);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
        
    //    [TestMethod]
    //    public async Task TestSendGrid()
    //    {
    //        string from = "Gonza_28.00@hotmail.com";
    //        string subject = "Sending with SendGrid is Fun";
    //        var to = new string[] { "gonzaloiba.28.00@gmail.com" };
    //        string content = "test";
    //        Message message = new Message(to,subject,content,from);

    //        var result = await _emailSender.SendEmailAsync(message);
    //    }

    //}
}
