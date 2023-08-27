using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.Helpers.ResultClasses;

namespace Transversal.EmailService.Contracts
{
    public interface IGenericEmailService
    {
        void SendEmail(Message message);
        Task<IGenericResult<string>> SendEmailAsync(Message message);
    }
}
