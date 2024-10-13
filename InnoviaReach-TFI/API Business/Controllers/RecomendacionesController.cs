using AutoMapper;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Business.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class RecomendacionesController : ControllerBase
    {
        private readonly IRecomendacionesService _recomendacionesService;

        public RecomendacionesController(
            IRecomendacionesService recomendacionesService)
        {
            _recomendacionesService = recomendacionesService;
        }

        [HttpGet("ObtenerRecomendaciones")]
        public async Task<IActionResult> ObtenerRecomendaciones(string user_id)
        {
            try
            {
               var result = await _recomendacionesService.RecomendacionesPorVisitas(user_id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
