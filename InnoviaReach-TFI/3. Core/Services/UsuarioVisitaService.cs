using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Models.Nueva_Base;
using Core.Domain.Request.Gateway;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class UsuarioVisitaService : GenericService<UsuarioVisitaModel>, IUsuarioVisitaService
    {
        private readonly IVideojuegoRepository _videojuegoRepo;
        private readonly IUsersRepository _usersRepo;
        private readonly IVideojuegoService _videojuegoService;
        private readonly IRecomendacionService _recomendacionService;
        private readonly IRecomendacionUsuarioService _recomendacionUsuarioService;
        private readonly IRecomendacionVideojuegoRepository _recomendacionVideojuegoRepository;

        public UsuarioVisitaService(IUnitOfWork unitOfWork, IVideojuegoService videojuegoService, IRecomendacionService recomendacionService, IRecomendacionUsuarioService recomendacionUsuarioService)
            : base(unitOfWork, unitOfWork.GetRepository<IUsuarioVisitaRepository>())
        {
            _recomendacionVideojuegoRepository = unitOfWork.GetRepository<IRecomendacionVideojuegoRepository>();
            _videojuegoRepo = unitOfWork.GetRepository<IVideojuegoRepository>();
            _usersRepo = unitOfWork.GetRepository<IUsersRepository>();
            _videojuegoService = videojuegoService;
            _recomendacionService = recomendacionService;
            _recomendacionUsuarioService = recomendacionUsuarioService;
        }

        public async Task RegistrarVista(UsuarioVisitaModel visita, string videojuego, string usuario)
        {
            try
            {
                var videojuegoEncontrado = (await _videojuegoRepo.Get(x => x.Slug == videojuego)).FirstOrDefault();

                if (videojuegoEncontrado is null)
                {
                    HttpClient httpClient = new HttpClient();

                    string apiUrl = "https://api.rawg.io/api/games/" + videojuego + "?key=6c43806ec84d4f09a9ac4c221d783da2";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON a la clase RawgApiResponse
                        VideojuegoRAWG apiResponse = JsonConvert.DeserializeObject<VideojuegoRAWG>(jsonResponse);

                        var videojuegoBase = await _videojuegoService.RegistrarObtenerVideojuego(apiResponse);

                        visita.Videojuego_ID = videojuegoBase.Videojuego_ID;
                    }
                }
                else
                {
                    visita.Videojuego_ID = videojuegoEncontrado.Videojuego_ID;
                }

                if (visita.Videojuego_ID != 0)
                {
                    visita.User_ID = (await _usersRepo.Get(x => x.UserName == usuario)).FirstOrDefault().Id;

                    await _repository.Insert(visita);

                    await _unitOfWork.SaveChangesAsync();
                }

                if (videojuegoEncontrado != null)
                {
                    await _recomendacionService.GenerarRecomendaciones(videojuegoEncontrado, visita.User_ID, "Contenido");
                }
                else
                {
                    var vidjuego = (await _videojuegoRepo.Get(x => x.Videojuego_ID == visita.Videojuego_ID)).FirstOrDefault();
                    await _recomendacionService.GenerarRecomendaciones(vidjuego, visita.User_ID, "Contenido");
                }

            }
            catch (Exception)
            {
                throw new Exception($"Error al registrar visita del usaurio {usuario} sobre el videojuego {videojuego}");
            }
        }
    }
}
