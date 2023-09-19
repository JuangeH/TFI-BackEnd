using API_Business.Request;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
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

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger,
            IVideojuegoService videojuegoService,
            IPlataformaService plataformaService)
        {
            _mapper = mapper;
            _logger = logger;
            _videojuegoService = videojuegoService;
            _plataformaService = plataformaService;
        }

        [HttpPost("RegistrarVideojuegos")]
        public async Task<IActionResult> RegistrarVideojuegos()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                Application videojuegosSteam = new Application();

                videojuegosSteam = await httpClient.GetFromJsonAsync<Application>("https://api.steampowered.com/ISteamApps/GetAppList/v2/");

                foreach (var item in videojuegosSteam.applist.apps)
                {
                    var response = await httpClient.GetAsync("http://store.steampowered.com/api/appdetails?appids=" + item.appid);
                    string json = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the JSON string
                        JObject jsonObject = JObject.Parse(json);

                        // Extract values
                        string type = null;
                        //type = (string)jsonObject[item.appid.ToString()]["data"]["type"];

                        if (jsonObject != null && jsonObject[item.appid.ToString()] != null && jsonObject[item.appid.ToString()]["data"] != null && jsonObject[item.appid.ToString()]["data"]["type"] != null)
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
                            videojuego.Nombre= jsonObject[item.appid.ToString()]["data"]["name"].ToString();
                            videojuego.Plataforma_ID = 1;

                            JArray categoriesArray = (JArray)jsonObject[item.appid.ToString()]["data"]["categories"];
                            JArray genresArray = (JArray)jsonObject[item.appid.ToString()]["data"]["genres"];

                            //_videojuegoService.CreateAsync(videojuego);

                            _videojuegoService.RegistrarVideojuego(videojuego, categoriesArray, genresArray);
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

        [HttpGet("ObtenerVideojuegos")]
        public async Task<IActionResult> ObtenerVideojuegos()
        {
            try
            {
                var resultado = await _videojuegoService.ObtenerVideojuegosYPlataformas();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }
    }
}
