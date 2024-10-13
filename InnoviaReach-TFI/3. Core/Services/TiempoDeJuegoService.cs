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
    public class TiempoDeJuegoService : GenericService<TiempoDeJuegoModel>, ITiempoDeJuegoService
    {
        private readonly IVideojuegoRepository _videojuegoRepository;

        public TiempoDeJuegoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<ITiempoDeJuegoRepository>())
        {
            _videojuegoRepository = unitOfWork.GetRepository<IVideojuegoRepository>();
        }

        public async Task<List<TiempoDeJuegoModel>> ObtenerTiemposDeJuego()
        {
            throw new NotImplementedException();
        }

        public async Task<TiempoDeJuegoModel> ObtenerTiemposDeJuegoUsuario(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task RegistrarTiempoDeJuego(TiempoDeJuegoModel tiempoDeJuegoModel, string UserID)
        {
            try
            {
                var videojuegoID = (await _videojuegoRepository.GetOne(x => x.Nombre == tiempoDeJuegoModel.videojuego.Nombre)).Videojuego_ID;
                tiempoDeJuegoModel.Videojuego_ID = videojuegoID;

                tiempoDeJuegoModel.User_ID = UserID;

                await _repository.Insert(tiempoDeJuegoModel);

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
