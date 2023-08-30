using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class PlataformaService : GenericService<PlataformaModel>, IPlataformaService
    {
        public PlataformaService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IPlataformaRepository>())
        {

        }
    }
}
