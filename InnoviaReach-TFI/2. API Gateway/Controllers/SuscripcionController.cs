using AutoMapper;
using Core.Domain.Helper;
using Core.Domain.Request.Business;
using Core.Domain.Request.Gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _2._API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class SuscripcionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SuscripcionController> _logger;
        private string ApiBaseURL = "https://api-business-apibusiness.onrender.com/";

        public SuscripcionController(
            IMapper mapper,
            ILogger<SuscripcionController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ValidarSuscripcion")]
        public async Task<IActionResult> ValidarSuscripcion()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Suscripcion/ValidarSuscripcion?User_ID={userid}";
                var GenericApiResponse = await RequestHelper.GetRequest<bool>(URL);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar suscripción");
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("ActualizarSuscripcion")]
        public async Task<IActionResult> ActualizarSuscripcion()
        {
            try
            {
                string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string URL = ApiBaseURL + $"Suscripcion/ActualizarSuscripcion";
                var GenericApiResponse = await RequestHelper.PutRequest<bool, string>(URL, userid);
                return Ok(GenericApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar suscripción del usuario");
                return BadRequest(new {ex.Message});
            }
        }
    }
}
