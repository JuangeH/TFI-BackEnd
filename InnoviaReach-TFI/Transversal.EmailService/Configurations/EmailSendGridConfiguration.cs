using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.EmailService.Configurations
{
    public class EmailSendGridConfiguration
    {
        public string ApiKey { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string ApiKeyId { get; set; }
    }
}
