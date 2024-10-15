using _2._API.Response;
using Core.Contracts.Services;
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
        private string ApiBaseURL = "https://localhost:44309/";
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
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }
    }
}
