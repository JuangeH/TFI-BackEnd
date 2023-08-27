using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.EmailService.Configurations
{
    public class EmailSMTPConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseSsl { get; set; }
        public string DisplayName { get; set; }
    }
}
