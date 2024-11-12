using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IRecomendacionUsuarioService : IGenericService<RecomendacionUsuarioModel>
    {
        public Task RegistrarRecomendacion(List<VideojuegoClusterModel> juegos);
    }
}
