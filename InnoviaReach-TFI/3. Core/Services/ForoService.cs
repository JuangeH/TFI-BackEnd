using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Core.Domain.Request.Gateway;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Extensions;

namespace _3._Core.Services
{
    public class ForoService : GenericService<ForoModel>, IForoService
    {
        private IRecomendacionVideojuegoRepository _recomendacionVideojuegoRepository;
        private IVideojuegoRepository _videojuegoRepository;
        private IRecomendacionService _recomendacionService;
        private readonly ILogger<ComentarioService> _logger;

        public ForoService(IUnitOfWork unitOfWork, IRecomendacionService recomendacionService, ILogger<ComentarioService> logger)
            : base(unitOfWork, unitOfWork.GetRepository<IForoRepository>())
        {
            _recomendacionVideojuegoRepository = unitOfWork.GetRepository<IRecomendacionVideojuegoRepository>();
            _videojuegoRepository = unitOfWork.GetRepository<IVideojuegoRepository>();
            _recomendacionService = recomendacionService;
            _logger = logger;
        }

        public async Task GestionarForoFavorito(GuardarForoRequest foro)
        {
            try
            {
                if (!(await _repository.Get(x => x.Foro_ID == foro.Foro_ID)).Any())
                {
                    throw new Exception("No se encuentra el foro");
                }

                var forofavoritoRepository = _unitOfWork.GetRepository<IForoUsuarioFavoritoRepository>();

                var foroFavoritoModel = (await forofavoritoRepository.Get(x => x.Foro_ID == foro.Foro_ID && x.User_ID == foro.User_ID)).FirstOrDefault();

                if (foroFavoritoModel is null)
                {
                    ForoUsuarioFavoritoModel model = new ForoUsuarioFavoritoModel();
                    model.Foro_ID = foro.Foro_ID;
                    model.User_ID = foro.User_ID;
                    await forofavoritoRepository.Insert(model);
                }
                else
                {
                    await forofavoritoRepository.Delete(foroFavoritoModel);
                }

                await _unitOfWork.SaveChangesAsync();

                var result = (await _repository.Get(x => x.Foro_ID == foro.Foro_ID)).FirstOrDefault();

                if (result != null)
                {
                    var vidjuego = (await _videojuegoRepository.Get(x => x.Videojuego_ID == result.Videojuego_ID)).FirstOrDefault();
                    await _recomendacionService.GenerarRecomendaciones(vidjuego, foro.User_ID, "Foro");
                }
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar administrar como favorito el foro {foro.Foro_ID} por el usuario {foro.User_ID}");
            }
        }

        public async Task<ForoModel> ObtenerForo(int id)
        {
            try
            {
                var result = (await _repository.GetOne(x => x.Foro_ID == id, includeProperties: "foroUsuarioVisitaModels,videojuego,usuario,comentarioModels"));

                return result;
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar obtener el foro {id}");
            }
            
        }

        public async Task<List<ForoModel>> ObtenerForosGenerales(string user_id)
        {
            try
            {
                var result = (await _repository.Get(includeProperties: "foroUsuarioFavoritoModels, foroUsuarioVisitaModels, comentarioModels, videojuego,usuario")).OrderByDescending(x => x.foroUsuarioVisitaModels.Count).ToList();

                return result;
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar obtener foros generales");
            }
            
        }
        public async Task RegistrarForo(ForoRequest foro)
        {
            try
            {
                await _repository.Insert(new ForoModel { User_ID = foro.User_ID, Videojuego_ID = foro.Videojuego_Codigo, Descripcion = foro.Descripcion, FechaCreado = foro.FechaCreado, Titulo=foro.Titulo, Activo=foro.Activo });
                _logger.LogBusiness($"El usuario [{foro.User_ID}] creó un nuevo foro.");
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar registrar foro");
            }
        }
        public async Task EliminarForo(int id)
        {
            try
            {
                var result = (await _repository.GetOne(x => x.Foro_ID == id));
                await _repository.Delete(result);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al intentar eliminar el foro {id}");
            }
        }
    }
}
