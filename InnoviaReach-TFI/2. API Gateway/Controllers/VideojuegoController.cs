using _3._Core.Services;
using Api.Controllers;
using Api.Request;
using API_Business.Request;
using API_Business.Response;
using AutoMapper;
using Core.Contracts.Data;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Core.Domain.Models;
using Core.Domain.Request.Gateway;
using Core.Domain.Response.Business;
using Core.Domain.Response.Gateway;
using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Security.Claims;
using Transversal.Helpers.JWT;
using static System.Net.WebRequestMethods;
using SteamInfoRequest = API_Business.Request.SteamInfoRequest;

namespace _2._API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VideojuegoController> _logger;
        private string ApiBaseURL = "https://api-business-apibusiness.onrender.com/";

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("RegistrarInformacion")]
        [Authorize]
        public async Task<IActionResult> RegistrarInformacion(SteamInfoRequest steamInfoRequest)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                string URL = ApiBaseURL + $"Videojuego/RegistrarInformacion/{userid}";
                var GenericApiResponse = await RequestHelper.PostRequest<bool, SteamInfoRequest>(URL, steamInfoRequest);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando registrar información de Steam");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerVideojuegosForo")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerVideojuegosForo()
        {
            try
            {
                string URL = ApiBaseURL + $"Videojuego/ObtenerVideojuegosForo";
                var GenericApiResponse = await RequestHelper.GetRequest<List<VideojuegoForoReponse>>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener videojuegos por foro");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerVideojuegosCatalogo")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerVideojuegosCatalogo(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                // Construir la URL con los parámetros de paginación
                string URL = ApiBaseURL + $"Videojuego/ObtenerVideojuegosCatalogo?pageNumber={pageNumber}&pageSize={pageSize}";

                // Hacer la solicitud a la API interna usando los parámetros de paginación
                var GenericApiResponse = await RequestHelper.GetRequest<PaginationResponse<VideojuegoCatalogoResponse>>(URL);

                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener videojuegos del catálogo");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegistrarVista/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarVista(string username, string videojuego)
        {
            try
            {
                UsuarioVisitaRequest usuarioVisitaRequest = new UsuarioVisitaRequest();
                usuarioVisitaRequest.videojuego = videojuego;

                // Construir la URL con los parámetros de paginación
                string URL = ApiBaseURL + $"Videojuego/RegistrarVisita/{username}";

                // Hacer la solicitud a la API interna usando los parámetros de paginación
                var GenericApiResponse = await RequestHelper.PostRequest<bool, UsuarioVisitaRequest>(URL, usuarioVisitaRequest);

                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al registrar visita de foro del usuario {username}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerVideojuegoDetalleCatalogo")]
        [Authorize]
        public async Task<IActionResult> ObtenerVideojuegoDetalleCatalogo(string nombre)
        {
            try
            {
                // Construir la URL con los parámetros de paginación
                string URL = ApiBaseURL + $"Videojuego/ObtenerVideojuegoDetalleCatalogo?nombre={nombre}";

                // Hacer la solicitud a la API interna usando los parámetros de paginación
                var GenericApiResponse = await RequestHelper.GetRequest<VideojuegoCatalogoDetalleResponse>(URL);

                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalle del videojuego {nombre}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("BuscarVideojuegoForo")]
        public async Task<IActionResult> BuscarVideojuegosForo(string nombre, int pageSize)
        {
            try
            {
                string URL = ApiBaseURL + $"Videojuego/BuscarVideojuegosForo?nombre={nombre}&pageSize={pageSize}";

                var GenericApiResponse = await RequestHelper.GetRequest<List<VideojuegoForoReponse>>(URL);

                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener videojuegos");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ObtenerVideojuegoForo")]
        public async Task<IActionResult> ObtenerVideojuegoForo(string nombre)
        {
            try
            {
                string URL = ApiBaseURL + $"Videojuego/ObtenerVideojuegoForo?nombre={nombre}";
                var GenericApiResponse = await RequestHelper.GetRequest<VideojuegoForoReponse>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener videojuegos");
                return BadRequest(ex.Message);
            }
        }

    }
}