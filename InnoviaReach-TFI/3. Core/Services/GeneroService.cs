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
    public class GeneroService : GenericService<GeneroModel>, IGeneroService
    {
        public GeneroService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IGeneroRepository>())
        {

        }

        public async Task<List<GeneroModel>> ObtenerGeneros()
        {
            try
            {
                return (await _repository.Get(x => x.Genero_ID.ToString() != "")).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
