using API_Business.Request;
using Core.Domain.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IAdquisicionService : IGenericService<AdquisicionModel>
    {
        public Task<List<AdquisicionModel>> ObtenerAdquisiciones();
        public Task<AdquisicionModel> ObtenerAdquisicionesUsuario(string UserName);
        public Task RegistrarAdquisicion(AdquisicionModel adquisicion, string UserID, int appid);
        public Task ActualizarJugadoReciente(SteamInfoRequest steamInfoRequest, string userid);
        public Task ActualizarRegistrarAdquisiciones(SteamInfoRequest steamInfoRequest, string userid);
    }
}
