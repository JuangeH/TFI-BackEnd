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
    public class ForoService : GenericService<ForoModel>, IForoService
    {
        public ForoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IForoRepository>())
        {

        }
        public async Task<List<ForoModel>> ObtenerForosGenerales() => (await _repository.Get()).OrderByDescending(x => x.Visitas).ToList();
    }
}
