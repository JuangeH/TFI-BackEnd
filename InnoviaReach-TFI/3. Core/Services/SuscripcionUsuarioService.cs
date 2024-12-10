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
    public class SuscripcionUsuarioService : GenericService<SuscripcionUsuarioModel>, ISuscripcionUsuarioService
    {
        private readonly ISuscripcionRepository _suscripcionRepository;

        public SuscripcionUsuarioService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<ISuscripcionUsuarioRepository>())
        {
            _suscripcionRepository = unitOfWork.GetRepository<ISuscripcionRepository>();
        }

        public async Task ActualizarSuscripcion(string UserID)
        {
            try
            {
                var suscripcionUsuario = await _repository.Get(x => x.User_ID == UserID);

                if (suscripcionUsuario.Any() is false)
                {
                    SuscripcionUsuarioModel suscripcionUsuarioModel = new SuscripcionUsuarioModel();
                    suscripcionUsuarioModel.User_ID = UserID;
                    suscripcionUsuarioModel.Suscripcion_ID = (await _suscripcionRepository.Get(x => x.Descripcion == "Premium")).FirstOrDefault().Suscripcion_ID;
                    suscripcionUsuarioModel.FechaInicio = DateTime.Now;
                    suscripcionUsuarioModel.FechaFin = DateTime.Now.AddMonths(1);
                    await _repository.Insert(suscripcionUsuarioModel);
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    await _repository.Delete(suscripcionUsuario);
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al actualizar la suscripción del usuario {UserID}");
            }
        }

        public async Task<bool> ValidarSuscripcion(string UserID)
        {
            try
            {
                bool encontrado = false;

                var suscripcionUsuario = await _repository.Get(x => x.User_ID == UserID);

                if (suscripcionUsuario.Any() is true)
                {
                    encontrado = true;
                }
                else
                {
                    encontrado = false;
                }

                return encontrado;
            }
            catch (Exception)
            {
                throw new Exception($"Error al validar la suscripción del usuario {UserID}");
            }
        }
    }
}
