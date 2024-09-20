using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Core.Domain.Request.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class ForoService : GenericService<ForoModel>, IForoService
    {
        public ForoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IForoRepository>())
        {

        }

        public async Task GestionarForoFavorito(GuardarForoRequest foro)
        {
            if (!(await _repository.Get(x=>x.Foro_ID==foro.Foro_ID)).Any())
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
        }

        public async Task<ForoModel> ObtenerForo(int id)
        {
            var result = (await _repository.GetOne(x => x.Foro_ID == id, includeProperties: "foroUsuarioVisitaModels,videojuego,usuario,comentarioModels"));

            return result;
        }

        public async Task<List<ForoModel>> ObtenerForosGenerales(string user_id)
        {
            var result = (await _repository.Get(includeProperties: "foroUsuarioFavoritoModels, foroUsuarioVisitaModels, comentarioModels, videojuego,usuario")).OrderByDescending(x => x.foroUsuarioVisitaModels.Count).ToList();

            return result;
        }
        public async Task RegistrarForo(ForoRequest foro)
        {
            try
            {
                await _repository.Insert(new ForoModel { User_ID = foro.User_ID, Videojuego_ID = foro.Videojuego_Codigo, Descripcion = foro.Descripcion, FechaCreado = foro.FechaCreado, Titulo=foro.Titulo, Activo=foro.Activo });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _unitOfWork.SaveChanges();
            }
        }
    }
}
