using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface ISuscripcionUsuarioService : IGenericService<SuscripcionUsuarioModel>
    {
        public Task<bool> ValidarSuscripcion(string UserID);
        public Task ActualizarSuscripcion(string UserID);
    }
}
