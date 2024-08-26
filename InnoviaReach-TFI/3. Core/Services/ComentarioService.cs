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
    public class ComentarioService : GenericService<ComentarioModel>, IComentarioService
    {
        public ComentarioService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IComentarioRepository>())
        {

        }
        public async Task<List<ComentarioModel>> ObtenerComentariosPorForo(int ForoId)
        {
            var result = (await _repository.Get(x => x.Foro_ID == ForoId, includeProperties: "usuario, puntuacionModels")).ToList();


            return result;
        }
    }
}
