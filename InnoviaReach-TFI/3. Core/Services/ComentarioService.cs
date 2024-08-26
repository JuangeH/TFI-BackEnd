using API_Business.Request;
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

        public async Task<bool> CalificarComentario(CalificarComentarioRequest request)
        {
            try
            {
                var comentario = (await _repository.Get(x => x.Comentario_ID == request.Comentario_ID)).FirstOrDefault();
                if (comentario is null)
                {
                    throw new Exception("No existe comentario para aplicar puntaje");
                }
                else if (comentario.User_ID == request.User_ID)
                {
                    throw new Exception("No se puede puntuar comentarios propios");
                }

                var _puntajeRepository = _unitOfWork.GetRepository<IPuntuacionRepository>();
                var puntuacion = (await _puntajeRepository.Get(x => x.User_ID == request.User_ID && x.Comentario_ID == comentario.Comentario_ID)).FirstOrDefault();

                if (puntuacion is null)
                {
                    await _puntajeRepository.Insert(new PuntuacionModel { Comentario_ID = comentario.Comentario_ID, User_ID=request.User_ID, Puntaje = request.Puntaje});
                }
                else
                {
                    puntuacion.Puntaje = request.Puntaje;
                    await _puntajeRepository.Update(puntuacion);
                }
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ComentarioModel>> ObtenerComentariosPorForo(int ForoId)
        {
            var result = (await _repository.Get(x => x.Foro_ID == ForoId, includeProperties: "usuario, puntuacionModels")).ToList();


            return result;
        }
    }
}
