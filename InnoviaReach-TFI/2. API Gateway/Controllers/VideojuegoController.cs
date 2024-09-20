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
using Core.Domain.Response.Business;
using Core.Domain.Response.Gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private string ApiBaseURL = "https://localhost:44309/";

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
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string URL = ApiBaseURL + $"Videojuego/RegistrarInformacion/{userid}";
            var GenericApiResponse = await RequestHelper.PostRequest<bool, SteamInfoRequest>(URL, steamInfoRequest);
            return Ok(GenericApiResponse);
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
                _logger.LogError(ex, "Error while obtaining videogames.");
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
                var GenericApiResponse = await RequestHelper.GetRequest<PaginationResponse<VideojuegoForoReponse>>(URL);

                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }

    }
}