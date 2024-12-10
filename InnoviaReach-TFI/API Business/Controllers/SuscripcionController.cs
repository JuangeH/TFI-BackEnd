using _3._Core.Services;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.Request.Gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Business.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class SuscripcionController : ControllerBase
    {
        private readonly ISuscripcionUsuarioService _suscripcionUsuarioService;
        private readonly IMapper _mapper;
        private readonly ILogger<SuscripcionController> _logger;

        public SuscripcionController(
            ISuscripcionUsuarioService suscripcionUsuarioService,IMapper mapper, ILogger<SuscripcionController> logger)
        {
            _suscripcionUsuarioService = suscripcionUsuarioService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ValidarSuscripcion")]
        public async Task<IActionResult> ValidarSuscripcion(string user_id)
        {
            try
            {
                var result = await _suscripcionUsuarioService.ValidarSuscripcion(user_id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut("ActualizarSuscripcion")]
        public async Task<IActionResult> ActualizarSuscripcion([FromBody] string user_id)
        {
            try
            {
                await _suscripcionUsuarioService.ActualizarSuscripcion(user_id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

