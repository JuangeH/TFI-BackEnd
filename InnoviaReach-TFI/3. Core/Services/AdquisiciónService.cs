using API_Business.Request;
using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3._Core.Services
{
    public class AdquisiciónService : GenericService<AdquisicionModel>, IAdquisicionService
    {
        private readonly IVideojuegoRepository _videojuegoRepository;
        private readonly IVideojuegoService _videojuegoService;
        private readonly IAdquisicionRepository _adquisicionRepository;

        public AdquisiciónService(IUnitOfWork unitOfWork, IVideojuegoService videojuegoService)
            : base(unitOfWork, unitOfWork.GetRepository<IAdquisicionRepository>())
        {
            _adquisicionRepository = unitOfWork.GetRepository<IAdquisicionRepository>();
            _videojuegoRepository = unitOfWork.GetRepository<IVideojuegoRepository>();
            _videojuegoService = videojuegoService;
        }

        public async Task ActualizarJugadoReciente(SteamInfoRequest steamInfoRequest, string userid)
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var recentlyPlayedGamesResponse = await httpClient.GetAsync("https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");
                string recentlyPlayedGamesjson = await recentlyPlayedGamesResponse.Content.ReadAsStringAsync();
                JObject recentlyPlayedGamesObject = JObject.Parse(recentlyPlayedGamesjson);

                Dictionary<string, string> excepciones = new Dictionary<string, string>();

                await _adquisicionRepository.LimpiarJuegoReciente(userid);

                foreach (var recentGame in recentlyPlayedGamesObject["response"]["games"].ToList())
                {

                    try
                    {
                        var videojuego = await _videojuegoService.RegistrarObtenerVideojuego(Convert.ToInt32(recentGame["appid"]));

                        if (videojuego != null)
                        {
                            var adquisicion = (await _repository.Get(x => x.Videojuego_ID == videojuego.Videojuego_ID && x.User_ID == userid)).FirstOrDefault();


                            if (adquisicion != null)
                            {
                                adquisicion.TiempoJuegoReciente = Convert.ToInt32(recentGame["playtime_forever"]);

                                await _repository.Update(adquisicion);

                                await _unitOfWork.SaveChangesAsync();
                            }
                            else
                            {
                                //AGREGAR METODO DE REGISTRAR ACTUALIZAR ADQUISICIONES

                            }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task ActualizarRegistrarAdquisiciones(SteamInfoRequest steamInfoRequest, string userid)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Root videojuegosAdquiridos = new Root();

                videojuegosAdquiridos = await httpClient.GetFromJsonAsync<Root>("https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");

                var recentlyPlayedGamesResponse = await httpClient.GetAsync("https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");
                string recentlyPlayedGamesjson = await recentlyPlayedGamesResponse.Content.ReadAsStringAsync();
                JObject recentlyPlayedGamesObject = JObject.Parse(recentlyPlayedGamesjson);

                Dictionary<string, string> excepciones = new Dictionary<string, string>();

                foreach (var item in videojuegosAdquiridos.response.games)
                {
                    try
                    {
                        var videojuego = await _videojuegoService.RegistrarObtenerVideojuego(item.appid);

                        if (videojuego != null)
                        {
                            var playerSummariesResponse = await httpClient.GetAsync("http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + item.appid + "&key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID);

                            string playerSummariesjson = await playerSummariesResponse.Content.ReadAsStringAsync();

                            JObject playerSummariesjsonObject = JObject.Parse(playerSummariesjson);

                            AdquisicionModel adquisicion = new AdquisicionModel();

                            if (Convert.ToBoolean(playerSummariesjsonObject["playerstats"]["success"]))
                            {
                                if (playerSummariesjsonObject["playerstats"]["achievements"] != null)
                                {
                                    adquisicion.CantidadLogros = playerSummariesjsonObject["playerstats"]["achievements"].Count();
                                }
                                else
                                    adquisicion.CantidadLogros = 0;
                            }
                            else
                                adquisicion.CantidadLogros = 0;

                            adquisicion.TiempoJuego = item.playtime_forever;

                            await RegistrarAdquisicion(adquisicion, userid, item.appid);
                        }

                    }
                    catch (Exception ex)
                    {
                        excepciones.Add(item.appid.ToString(), ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<AdquisicionModel>> ObtenerAdquisiciones()
        {
            throw new NotImplementedException();
        }

        public async Task<AdquisicionModel> ObtenerAdquisicionesUsuario(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task RegistrarAdquisicion(AdquisicionModel adquisicion, string UserID, int appid)
        {
            try
            {
                var videojuego = (await _videojuegoRepository.Get(x => x.SteamAppid == appid)).FirstOrDefault();

                if (videojuego != null)
                {
                    adquisicion.Videojuego_ID = videojuego.Videojuego_ID;

                    adquisicion.User_ID = UserID;

                    var adquisicionModel = (await _repository.Get(x => x.Videojuego_ID == adquisicion.Videojuego_ID && x.User_ID == adquisicion.User_ID)).FirstOrDefault();

                    if (adquisicionModel is null)
                    {
                        await _repository.Insert(adquisicion);

                        await _unitOfWork.SaveChangesAsync();
                    }
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
