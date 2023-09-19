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
    public class EstiloService : GenericService<EstiloModel>, IEstiloService
    {
        public EstiloService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IEstiloRepository>())
        {

        }

        public async Task<List<EstiloModel>> ObtenerEstilos()
        {
            try
            {
                return (await _repository.Get(x => x.Estilo_ID.ToString() != "")).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
