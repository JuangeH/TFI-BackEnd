using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.EmailService.Contracts;
using Transversal.Helpers.ResultClasses;

namespace Transversal.EmailService.Services
{
    public abstract class GenericEmailService : IGenericEmailService
    {
        public abstract void SendEmail(Message message);
        public abstract Task<IGenericResult<string>> SendEmailAsync(Message message);
    }
}
