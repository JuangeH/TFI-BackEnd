using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Util;
using API_Business.Request;
using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Extensions;

namespace _3._Core.Services
{
    public class ComentarioService : GenericService<ComentarioModel>, IComentarioService
    {
        private readonly ILogger<ComentarioService> _logger;
        public ComentarioService(IUnitOfWork unitOfWork, ILogger<ComentarioService> logger)
            : base(unitOfWork, unitOfWork.GetRepository<IComentarioRepository>())
        {
            _logger = logger;
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

                _logger.LogBusiness($"El usuario [{request.User_ID}] calificó el comentario [{comentario.Comentario_ID}] con el puntaje [{request.Puntaje}]");

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

            try
            {
                var result = (await _repository.Get(x => x.Foro_ID == ForoId, includeProperties: "usuario, puntuacionModels")).OrderByDescending(x => x.FechaCreacion).ToList();
                return result;

            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar obtener comentarios del foro {ForoId}");
            }
            
        }

        public async Task RegistrarComentario(ComentarioRequest comentario)
        {
            try
            {
                if (comentario.ComentarioPadre_Codigo is null)
                {
                    await _repository.Insert(new ComentarioModel { User_ID = comentario.User_ID, Foro_ID = comentario.Foro_Codigo, Contenido = comentario.Contenido, FechaCreacion = comentario.FechaCreacion });

                }
                else
                {
                    await _repository.Insert(new ComentarioModel { User_ID = comentario.User_ID, Foro_ID = comentario.Foro_Codigo, ComentarioPadre_ID = comentario.ComentarioPadre_Codigo, Contenido = comentario.Contenido, FechaCreacion = comentario.FechaCreacion });
                }
                _logger.LogBusiness($"El usuario [{comentario.User_ID}] hizo un comentario en el foro [{comentario.Foro_Codigo}]");

                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar registrar comentario en el foro {comentario.Foro_Codigo}");
            }
        }

        public async Task EliminarComentario(int id)
        {
            try
            {
                var result = (await _repository.GetOne(x => x.Comentario_ID == id));
                await _repository.Delete(result);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar eliminar el comentario {id}");
            }
        }
    }
}
