using _3._Core.Services;
using API_Business.Request;
using API_Business.Response;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Response.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using static Google.Apis.Requests.BatchRequest;

namespace API_Business.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VideojuegoController> _logger;
        private readonly IVideojuegoService _videojuegoService;
        private readonly IPlataformaService _plataformaService;
        private readonly IAdquisicionService _adquisicionService;
        private readonly ISteamAccountService _steamAccountService;

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger,
            IVideojuegoService videojuegoService,
            IPlataformaService plataformaService,
            IAdquisicionService adquisicionService,
            ISteamAccountService steamAccountService)
        {
            _mapper = mapper;
            _logger = logger;
            _videojuegoService = videojuegoService;
            _plataformaService = plataformaService;
            _adquisicionService = adquisicionService;
            _steamAccountService = steamAccountService;
        }

        [HttpPost("RegistrarVideojuegos")]
        public async Task<IActionResult> RegistrarVideojuegos()
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                var videojuegosSteam = await httpClient.GetFromJsonAsync<Application>("https://api.steampowered.com/ISteamApps/GetAppList/v2/");

                if (videojuegosSteam?.applist?.apps == null)
                {
                    _logger.LogError("No se pudo obtener la lista de videojuegos de Steam.");
                    return BadRequest("No se pudo obtener la lista de videojuegos de Steam.");
                }

                foreach (var item in videojuegosSteam.applist.apps)
                {
                    await _videojuegoService.RegistrarObtenerVideojuego(item.appid);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegistrarInformacion/{userid}")]
        public async Task<IActionResult> RegistrarInformacion(SteamInfoRequest steamInfoRequest, string userid)
        {
            if (string.IsNullOrEmpty(steamInfoRequest.SteamAPIKey))
            {
                steamInfoRequest.SteamAPIKey = "DB87EDFDEF1A6EC905BD4F1F51B2377A";
            }
            try
            {
                HttpClient httpClient = new HttpClient();

                //STEAM ACCOUNT
                SteamAccountModel steamAccount = new SteamAccountModel();

                steamAccount.steamid = steamInfoRequest.SteamID;
                steamAccount.User_ID = userid;

                var steamAccountResponse = await httpClient.GetAsync("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + steamInfoRequest.SteamAPIKey + "&steamids=" + steamInfoRequest.SteamID);
                string steamAccountjson = await steamAccountResponse.Content.ReadAsStringAsync();
                JObject steamAccountsObject = JObject.Parse(steamAccountjson);

                steamAccount.avatarfull = steamAccountsObject["response"]["players"][0]["avatarfull"].ToString();
                steamAccount.profileurl = steamAccountsObject["response"]["players"][0]["profileurl"].ToString();
                steamAccount.personaname = steamAccountsObject["response"]["players"][0]["personaname"].ToString();

                await _steamAccountService.RegistrarUsuario(steamAccount);
                //STEAM ACCOUNT

                //VALIDAR NO REGISTRAR MÚLTIPLES VECES LA MISMA ADQUISICION/TIEMPO DE JUEGO

                await _adquisicionService.ActualizarRegistrarAdquisiciones(steamInfoRequest, userid);
                await _adquisicionService.ActualizarJugadoReciente(steamInfoRequest, userid);
                
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerVideojuegosCatalogo")]
        public async Task<IActionResult> ObtenerVideojuegosCatalogo(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                // Obtener los videojuegos paginados desde el servicio
                var resultado = await _videojuegoService.ObtenerVideojuegosCatalogo(pageNumber, pageSize);

                // Mapear a la respuesta esperada
                var response = _mapper.Map<List<VideojuegoResponse>>(resultado.Videojuegos);

                // Retornar también el total de registros
                return Ok(new
                {
                    TotalRecords = resultado.TotalRecords,
                    Videojuegos = response
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerVideojuegosForo")]
        public async Task<IActionResult> ObtenerVideojuegosForo()
        {
            try
            {
                var resultado = await _videojuegoService.ObtenerVideojuegos();
                var response = _mapper.Map<List<VideojuegoForoReponse>>(resultado);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }

    }
}

//[HttpPost("RegistrarInformacion/{userid}")]
//public async Task<IActionResult> RegistrarInformacion(SteamInfoRequest steamInfoRequest, string userid)
//{
//    try
//    {
//        HttpClient httpClient = new HttpClient();

//        //STEAM ACCOUNT
//        SteamAccountModel steamAccount = new SteamAccountModel();

//        steamAccount.steamid = steamInfoRequest.SteamID;
//        steamAccount.User_ID = userid;

//        var steamAccountResponse = await httpClient.GetAsync("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + steamInfoRequest.SteamAPIKey + "&steamids=" + steamInfoRequest.SteamID);
//        string steamAccountjson = await steamAccountResponse.Content.ReadAsStringAsync();
//        JObject steamAccountsObject = JObject.Parse(steamAccountjson);

//        steamAccount.avatarfull = steamAccountsObject["response"]["players"][0]["avatarfull"].ToString();
//        steamAccount.profileurl = steamAccountsObject["response"]["players"][0]["profileurl"].ToString();
//        steamAccount.personaname = steamAccountsObject["response"]["players"][0]["personaname"].ToString();

//        await _steamAccountService.RegistrarUsuario(steamAccount);
//        //STEAM ACCOUNT

//        //VALIDAR NO REGISTRAR MÚLTIPLES VECES LA MISMA ADQUISICION/TIEMPO DE JUEGO

//        Root videojuegosAdquiridos = new Root();

//        videojuegosAdquiridos = await httpClient.GetFromJsonAsync<Root>("https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");

//        var recentlyPlayedGamesResponse = await httpClient.GetAsync("https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");
//        string recentlyPlayedGamesjson = await recentlyPlayedGamesResponse.Content.ReadAsStringAsync();
//        JObject recentlyPlayedGamesObject = JObject.Parse(recentlyPlayedGamesjson);

//        Dictionary<string, string> excepciones = new Dictionary<string, string>();

//        foreach (var item in videojuegosAdquiridos.response.games)
//        {
//            try
//            {
//                var response = await httpClient.GetAsync("http://store.steampowered.com/api/appdetails?appids=" + item.appid);
//                var playerSummariesResponse = await httpClient.GetAsync("http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + item.appid + "&key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID);

//                string json = await response.Content.ReadAsStringAsync();
//                string playerSummariesjson = await playerSummariesResponse.Content.ReadAsStringAsync();

//                if (response.IsSuccessStatusCode && json != null)
//                {
//                    // Parse the JSON string
//                    JObject jsonObject = JObject.Parse(json);
//                    JObject playerSummariesjsonObject = JObject.Parse(playerSummariesjson);

//                    // Extract values
//                    string? type = default;
//                    //type = (string)jsonObject[item.appid.ToString()]["data"]["type"];

//                    if (jsonObject != null && jsonObject[item.appid.ToString()] != null && jsonObject[item.appid.ToString()]?["data"] != null && jsonObject[item.appid.ToString()]?["data"]?["type"] != null && jsonObject[item.appid.ToString()]?["data"]?["categories"] != null && jsonObject[item.appid.ToString()]?["data"]?["genres"] != null)
//                    {
//                        type = jsonObject[item.appid.ToString()]["data"]["type"].ToString();
//                    }
//                    else
//                    {
//                        type = null;
//                    }

//                    if (type == "game")
//                    {
//                        VideojuegoModel videojuego = new VideojuegoModel();
//                        AdquisicionModel adquisicion = new AdquisicionModel();

//                        videojuego.Nombre = jsonObject[item.appid.ToString()]["data"]["name"].ToString();
//                        videojuego.SteamAppid = item.appid;
//                        videojuego.Recomendaciones = Convert.ToInt32(jsonObject[item.appid.ToString()]?["data"]?["recommendations"]?["total"]);
//                        videojuego.Header_image = jsonObject[item.appid.ToString()]["data"]["header_image"].ToString();
//                        videojuego.Plataforma_ID = 1;

//                        if (jsonObject[item.appid.ToString()]?["data"]?["metacritic"] != null)
//                        {
//                            videojuego.Metacritic_score = Convert.ToInt32(jsonObject[item.appid.ToString()]?["data"]?["metacritic"]?["score"]);
//                            videojuego.Metacritic_url = jsonObject[item.appid.ToString()]?["data"]?["metacritic"]?["url"]?.ToString();
//                        }
//                        else
//                        {
//                            videojuego.Metacritic_score = 0;
//                            videojuego.Metacritic_url = "";
//                        }

//                        JArray categoriesArray = (JArray)jsonObject[item.appid.ToString()]["data"]["categories"];
//                        JArray genresArray = (JArray)jsonObject[item.appid.ToString()]["data"]["genres"];

//                        if (jsonObject[item.appid.ToString()]?["data"]?["metacritic"] != null)
//                        {
//                            videojuego.Metacritic_score = Convert.ToInt32(jsonObject[item.appid.ToString()]?["data"]?["metacritic"]?["score"]);
//                            videojuego.Metacritic_url = jsonObject[item.appid.ToString()]?["data"]?["metacritic"]?["url"]?.ToString();
//                        }
//                        else
//                        {
//                            videojuego.Metacritic_score = 0;
//                            videojuego.Metacritic_url = "";
//                        }

//                        if (Convert.ToBoolean(playerSummariesjsonObject["playerstats"]["success"]))
//                        {
//                            if (playerSummariesjsonObject["playerstats"]["achievements"] != null)
//                            {
//                                adquisicion.CantidadLogros = playerSummariesjsonObject["playerstats"]["achievements"].Count();
//                            }
//                            else
//                                adquisicion.CantidadLogros = 0;
//                        }
//                        else
//                            adquisicion.CantidadLogros = 0;


//                        foreach (var recentGame in recentlyPlayedGamesObject["response"]["games"].ToList())
//                        {
//                            if (Convert.ToInt32(recentGame["appid"]) == item.appid)
//                            {
//                                adquisicion.TiempoJuegoReciente = Convert.ToInt32(recentGame["playtime_forever"]);
//                            }
//                        }

//                        adquisicion.TiempoJuego = item.playtime_forever;

//                        await _videojuegoService.RegistrarVideojuegoEstiloGenero(videojuego, categoriesArray, genresArray);

//                        await _adquisicionService.RegistrarAdquisicion(adquisicion, userid, item.appid);

//                    }
//                }
//                else
//                {
//                    // Si la solicitud no fue exitosa, maneja el error aquí
//                }
//            }
//            catch (Exception ex)
//            {
//                excepciones.Add(item.appid.ToString(), ex.Message);
//            }

//        }

//        return Ok(excepciones);
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex, "Error while obtaining videogames.");
//        return BadRequest(ex.Message);
//    }
//}