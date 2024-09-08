using _2._API.Response;
using API_Business.Request;
using AutoMapper;
using Core.Domain.Helper;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Gateway.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ForoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ForoController> _logger;
        private string ApiBaseURL = "https://localhost:44309/";

        public ForoController(
            IMapper mapper,
            ILogger<ForoController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ObtenerForosGenerales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerForosGenerales()
        {
            string URL = ApiBaseURL + $"Foro/ObtenerForosGenerales";
            var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);
            return Ok(GenericApiResponse);
        }

        [HttpGet("ObtenerComentariosPorForo")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerComentariosPorForo(int ForoId)
        {
            string URL = ApiBaseURL + $"Foro/ObtenerComentariosPorForo?ForoId={ForoId}";
            var GenericApiResponse = await RequestHelper.GetRequest<List<ComentarioResponse>>(URL);
            return Ok(GenericApiResponse);
        }

        [HttpPost("CalificarComentario")]
        public async Task<IActionResult> CalificarComentario([FromBody] CalificarComentarioRequest request)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                request.User_ID = userid;

                string URL = ApiBaseURL + $"Foro/CalificarComentario";
                var GenericApiResponse = await RequestHelper.PostRequest<bool,CalificarComentarioRequest>(URL, request);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("RegistrarComentario")]
        public async Task<IActionResult> RegistrarComentario([FromBody] ComentarioRequest request)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                request.User_ID = userid;

                string URL = ApiBaseURL + $"Foro/RegistrarComentario";

                var GenericApiResponse = await RequestHelper.PostRequest<bool, ComentarioRequest>(URL, request);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando registrar comentario");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegistrarForo")]
        public async Task<IActionResult> RegistrarForo([FromBody] ForoRequest request)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                request.User_ID = userid;

                string URL = ApiBaseURL + $"Foro/RegistrarForo";

                var GenericApiResponse = await RequestHelper.PostRequest<bool, ForoRequest>(URL, request);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando registrar foro");
                return BadRequest(ex.Message);
            }
        }
    }
}
