using API_Business.Request;
using API_Business.Response;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Core.Domain.Models;
using Core.Domain.Request.Business;
using Core.Domain.Request.Gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Business.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ForoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ForoController> _logger;
        private readonly IForoService _foroService;
        private readonly IComentarioService _comentarioService;

        public ForoController(
            IMapper mapper,
            ILogger<ForoController> logger,
            IForoService foroService,
            IComentarioService comentarioService)
        {
            _mapper = mapper;
            _logger = logger;
            _foroService = foroService;
            _comentarioService = comentarioService;
        }

        [HttpGet("ObtenerForosGenerales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerForosGenerales(string user_id)
        {
            try
            {
                var result = await _foroService.ObtenerForosGenerales(user_id);
                var response = _mapper.Map<List<ForoResponse>>(result, opt => {
                    opt.Items["LoggedUserID"] = user_id;
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ObtenerComentariosPorForo")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerComentariosPorForo(int ForoId)
        {
            try
            {
                var result = await _comentarioService.ObtenerComentariosPorForo(ForoId);
                var response = _mapper.Map<List<ComentarioResponse>>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("CalificarComentario")]
        public async Task<IActionResult> CalificarComentario([FromBody] CalificarComentarioRequest request)
        {
            try
            {
                var result = await _comentarioService.CalificarComentario(request);
                return Ok(result);
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
                await _comentarioService.RegistrarComentario(request);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando registrar comentario.");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("RegistrarForo")]
        public async Task<IActionResult> RegistrarForo([FromBody] ForoRequest request)
        {
            try
            {
                await _foroService.RegistrarForo(request);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando registrar foro");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("GestionarForoFavorito")]
        public async Task<IActionResult> GestionarForoFavorito([FromBody] GuardarForoRequest request)
        {
            try
            {
                await _foroService.GestionarForoFavorito(request);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error intentando guardar foro");
                return BadRequest(ex.Message);
            }
        }
    }
}
