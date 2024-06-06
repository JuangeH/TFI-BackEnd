using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3._Core.Services
{
    public class AdquisiciónService : GenericService<AdquisicionModel>, IAdquisicionService
    {
        private readonly IVideojuegoRepository _videojuegoRepository;

        public AdquisiciónService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IAdquisicionRepository>())
        {
            _videojuegoRepository = unitOfWork.GetRepository<IVideojuegoRepository>();
        }

        public async Task<List<AdquisicionModel>> ObtenerAdquisiciones()
        {
            throw new NotImplementedException();
        }

        public async Task<AdquisicionModel> ObtenerAdquisicionesUsuario(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task RegistrarAdquisicion(AdquisicionModel adquisicion, string UserID)
        {
            try
            {
                adquisicion.Videojuego_ID = (await _videojuegoRepository.GetOne(x => x.Nombre == adquisicion.videojuego.Nombre)).Videojuego_ID; 

                adquisicion.User_ID = UserID;

                await _repository.Insert(adquisicion);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
