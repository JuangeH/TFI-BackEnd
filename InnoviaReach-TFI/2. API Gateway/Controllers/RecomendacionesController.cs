using _2._API.Response;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Core.Domain.Response.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2._API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class RecomendacionesController : ControllerBase
    {
        private string ApiBaseURL = "https://api-business-apibusiness.onrender.com/";
        private readonly ILogger<RecomendacionesController> _logger;

        public RecomendacionesController(
             ILogger<RecomendacionesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ObtenerRecomendacionesForoVisitado")]
        public async Task<IActionResult> ObtenerRecomendacionesForoVisitado()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerRecomendacionesForoVisitado?User_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando obterner recomendaciones por foro visitado");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerRecomendacionesIndividuales")]
        public async Task<IActionResult> ObtenerRecomendacionesIndividuales()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerRecomendacionesIndividuales?Usuario_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<RecomendacionVideojuegoResponse>>(URL);

                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando obterner recomendaciones individuales");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerRecomendacionesColaborativas")]
        public async Task<IActionResult> ObtenerRecomendacionesColaborativas()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerRecomendacionesColaborativas?Usuario_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<RecomendacionUsuarioResponse>>(URL);

                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando obterner recomendaciones colaborativas");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        [HttpGet("ObtenerForosRecomendadosVisita")]
        public async Task<IActionResult> ObtenerForosRecomendadosVisita()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerForosRecomendadosVisita?Usuario_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);

                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando obterner foros recomendados por visita");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerForosRecomendadosForosFav")]
        public async Task<IActionResult> ObtenerForosRecomendadosForosFav()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerForosRecomendadosForosFav?Usuario_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);

                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando obterner foros recomendados por favoritos");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerForosRecomendadosColab")]
        public async Task<IActionResult> ObtenerForosRecomendadosColab()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerForosRecomendadosColab?Usuario_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);

                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando obterner foros recomendados por colaboracion");
                return BadRequest(ex.Message);
            }
        }
    }
}
