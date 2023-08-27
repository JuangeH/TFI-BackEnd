using System;
using System.Collections.Generic;
using System.Text;
using Transversal.EmailService.Contracts;

namespace Transversal.EmailService.Factory
{
    public interface IGenericEmailFactory
    {
        IGenericEmailService GetDefault();

        IGenericEmailService Get(GenericEmailTypeEnum emailType);
    }
}
