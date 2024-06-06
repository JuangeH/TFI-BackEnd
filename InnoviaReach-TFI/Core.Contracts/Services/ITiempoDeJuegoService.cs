using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface ITiempoDeJuegoService : IGenericService<TiempoDeJuegoModel>
    {
        public Task<List<TiempoDeJuegoModel>> ObtenerTiemposDeJuego();
        public Task<TiempoDeJuegoModel> ObtenerTiemposDeJuegoUsuario(string UserName);
        public Task RegistrarTiempoDeJuego(TiempoDeJuegoModel adquisicion, string UserID);
    }
}
