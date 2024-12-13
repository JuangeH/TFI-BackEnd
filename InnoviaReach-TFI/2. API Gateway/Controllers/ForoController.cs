using _2._API.Response;
using API_Business.Request;
using AutoMapper;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Core.Domain.Request.Gateway;
using Core.Domain.Response.Business;
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
        private string ApiBaseURL = "https://api-business-apibusiness.onrender.com/";

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
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Foro/ObtenerForosGenerales?User_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoResponse>>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener foros generales");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerComentariosPorForo")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerComentariosPorForo(int ForoId)
        {
            try
            {
                string URL = ApiBaseURL + $"Foro/ObtenerComentariosPorForo?ForoId={ForoId}";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ComentarioResponse>>(URL);
                return Ok(GenericApiResponse);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"Error al obtener comentarios para el foro {ForoId}");
                return BadRequest(ex.Message);
            }

            
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
                _logger.LogError(ex, $"Error al calificar comentario {request.Comentario_ID}");
                return BadRequest(ex.Message);
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
                _logger.LogError(ex, $"Error intentando registrar comentario en el foro {request.Foro_Codigo}");
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

        [HttpPut("GestionarForoFavorito")]
        public async Task<IActionResult> GestionarForoFavorito([FromQuery] int CodForo)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                string URL = ApiBaseURL + $"Foro/GestionarForoFavorito";

                var GenericApiResponse = await RequestHelper.PutRequest<bool, GuardarForoRequest>(URL,new GuardarForoRequest { Foro_ID = CodForo, User_ID = userid });
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando guardar como favorito el foro {CodForo}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("RegistrarVisita")]
        public async Task<IActionResult> RegistrarVisita([FromBody] ForoUsuarioVisitaRequest request)
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                string URL = ApiBaseURL + $"Foro/RegistrarVisita";

                var GenericApiResponse = await RequestHelper.PostRequest<bool, ForoUsuarioVisitaRequest>(URL, new ForoUsuarioVisitaRequest { Foro_ID = request.Foro_ID, User_ID = userid });
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando registrar visita al foro {request.Foro_ID} por el usuario {request.User_ID}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("EliminarForo")]
        public async Task<IActionResult> EliminarForo(int ForoId)
        {
            try
            {
                string URL = ApiBaseURL + $"Foro/EliminarForo?ForoId={ForoId}";
                var GenericApiResponse = await RequestHelper.DeleteRequest<bool>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando eliminar el foro {ForoId}");
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("EliminarComentario")]
        public async Task<IActionResult> EliminarComentario(int ComentarioId)
        {
            try
            {
                string URL = ApiBaseURL + $"Foro/EliminarComentario?ComentarioId={ComentarioId}";
                var GenericApiResponse = await RequestHelper.DeleteRequest<bool>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error intentando eliminar comentario {ComentarioId}");
                return BadRequest(ex.Message);
            }
        }
    }
}
