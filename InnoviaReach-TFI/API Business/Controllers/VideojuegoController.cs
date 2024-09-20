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
        private readonly ITiempoDeJuegoService _tiempoDeJuegoService;

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger,
            IVideojuegoService videojuegoService,
            IPlataformaService plataformaService,
            IAdquisicionService adquisicionService,
            ITiempoDeJuegoService tiempoDeJuegoService)
        {
            _mapper = mapper;
            _logger = logger;
            _videojuegoService = videojuegoService;
            _plataformaService = plataformaService;
            _adquisicionService = adquisicionService;
            _tiempoDeJuegoService = tiempoDeJuegoService;
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
                    var response = await httpClient.GetAsync("http://store.steampowered.com/api/appdetails?appids=" + item.appid);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && json != null)
                    {
                        // Parse the JSON string
                        JObject jsonObject = JObject.Parse(json);

                        // Extract values
                        string? type = null;
                        //type = (string)jsonObject[item.appid.ToString()]["data"]["type"];

                        if (jsonObject != null && jsonObject[item.appid.ToString()] != null && jsonObject[item.appid.ToString()]?["data"] != null && jsonObject[item.appid.ToString()]?["data"]?["type"] != null && jsonObject[item.appid.ToString()]?["data"]?["categories"] != null && jsonObject[item.appid.ToString()]?["data"]?["genres"] != null)
                        {
                            type = jsonObject[item.appid.ToString()]["data"]["type"].ToString();
                        }
                        else
                        {
                            type = null;
                        }

                        if (type == "game")
                        {
                            VideojuegoModel videojuego = new VideojuegoModel();
                            videojuego.Nombre = jsonObject[item.appid.ToString()]["data"]["name"].ToString();
                            videojuego.Recomendaciones = Convert.ToInt32(jsonObject[item.appid.ToString()]?["data"]?["recommendations"]?["total"]);
                            videojuego.Plataforma_ID = 1;

                            JArray categoriesArray = (JArray)jsonObject[item.appid.ToString()]["data"]["categories"];
                            JArray genresArray = (JArray)jsonObject[item.appid.ToString()]["data"]["genres"];

                            await _videojuegoService.RegistrarVideojuego(videojuego, categoriesArray, genresArray);
                        }
                    }
                    else
                    {
                        // Si la solicitud no fue exitosa, maneja el error aquí
                    }


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
            try
            {
                //VALIDAR NO REGISTRAR MÚLTIPLES VECES LA MISMA ADQUISICION/TIEMPO DE JUEGO

                HttpClient httpClient = new HttpClient();

                Root videojuegosAdquiridos = new Root();

                videojuegosAdquiridos = await httpClient.GetFromJsonAsync<Root>("https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + steamInfoRequest.SteamAPIKey + "&steamid=" + steamInfoRequest.SteamID + "&format=json");


                foreach (var item in videojuegosAdquiridos.response.games)
                {
                    var response = await httpClient.GetAsync("http://store.steampowered.com/api/appdetails?appids=" + item.appid);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && json != null)
                    {
                        // Parse the JSON string
                        JObject jsonObject = JObject.Parse(json);

                        // Extract values
                        string? type = default;
                        //type = (string)jsonObject[item.appid.ToString()]["data"]["type"];

                        if (jsonObject != null && jsonObject[item.appid.ToString()] != null && jsonObject[item.appid.ToString()]?["data"] != null && jsonObject[item.appid.ToString()]?["data"]?["type"] != null && jsonObject[item.appid.ToString()]?["data"]?["categories"] != null && jsonObject[item.appid.ToString()]?["data"]?["genres"] != null)
                        {
                            type = jsonObject[item.appid.ToString()]["data"]["type"].ToString();
                        }
                        else
                        {
                            type = null;
                        }

                        if (type == "game")
                        {
                            VideojuegoModel videojuego = new VideojuegoModel();
                            AdquisicionModel adquisicion = new AdquisicionModel();
                            TiempoDeJuegoModel tiempoDeJuego = new TiempoDeJuegoModel();

                            videojuego.Nombre = jsonObject[item.appid.ToString()]["data"]["name"].ToString();
                            videojuego.Recomendaciones = Convert.ToInt32(jsonObject[item.appid.ToString()]?["data"]?["recommendations"]?["total"]);
                            videojuego.Plataforma_ID = 1;

                            JArray categoriesArray = (JArray)jsonObject[item.appid.ToString()]["data"]["categories"];
                            JArray genresArray = (JArray)jsonObject[item.appid.ToString()]["data"]["genres"];

                            adquisicion.videojuego = videojuego;
                            tiempoDeJuego.videojuego = videojuego;
                            tiempoDeJuego.CantidadMinutos = item.playtime_forever;

                            await _videojuegoService.RegistrarVideojuego(videojuego, categoriesArray, genresArray);

                            await _adquisicionService.RegistrarAdquisicion(adquisicion, userid);

                            await _tiempoDeJuegoService.RegistrarTiempoDeJuego(tiempoDeJuego, userid);

                        }
                    }
                    else
                    {
                        // Si la solicitud no fue exitosa, maneja el error aquí
                    }

                }

                return Ok(true);
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

