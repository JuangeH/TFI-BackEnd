using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IEmailService
    {
        Task RegistrationEmailAsync(Users user);

        Task ForgotPasswordSendEmail(Users user);
    }
}
