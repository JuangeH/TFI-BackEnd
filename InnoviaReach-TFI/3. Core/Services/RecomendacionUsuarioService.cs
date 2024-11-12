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
    public class RecomendacionUsuarioService : GenericService<RecomendacionUsuarioModel>, IRecomendacionUsuarioService
    {
        public RecomendacionUsuarioService(IUnitOfWork unitOfWork)
           : base(unitOfWork, unitOfWork.GetRepository<IRecomendacionUsuarioRepository>())
        {
            
        }

        public Task RegistrarRecomendacion(List<VideojuegoClusterModel> juegos)
        {
            throw new NotImplementedException();
        }
    }
}
