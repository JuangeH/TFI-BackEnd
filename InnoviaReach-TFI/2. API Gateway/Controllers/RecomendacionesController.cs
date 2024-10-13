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
        private readonly IRecomendacionesService _recomendacionesService;
        private string ApiBaseURL = "https://localhost:44309/";
        private readonly ILogger<RecomendacionesController> _logger;

        public RecomendacionesController(
             ILogger<RecomendacionesController> logger,
            IRecomendacionesService recomendacionesService)
        {
            _recomendacionesService = recomendacionesService;
            _logger = logger;
        }

        [HttpGet("ObtenerRecomendaciones")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerRecomendaciones()
        {
            try
            {
                string URL = ApiBaseURL + $"Recomendaciones/ObtenerRecomendaciones";
                var GenericApiResponse = await RequestHelper.GetRequest<List<ForoModel>>(URL);
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
