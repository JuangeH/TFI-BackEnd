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
    public class VideojuegoEstiloService : GenericService<VideojuegoEstiloModel>, IVideojuegoEstiloService
    {
        public VideojuegoEstiloService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IVideojuegoEstiloRepository>())
        {

        }

        public async Task RegistrarEstiloVid(VideojuegoEstiloModel videojuegoEstiloModel)
        {
            await _repository.Insert(videojuegoEstiloModel);
        }

    }
}
