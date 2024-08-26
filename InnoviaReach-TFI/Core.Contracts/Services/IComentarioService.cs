using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IComentarioService : IGenericService<ComentarioModel>
    {
        public Task<List<ComentarioModel>> ObtenerComentariosPorForo(int ForoId);
        public Task<bool> CalificarComentario(CalificarComentarioRequest);
    }
}
