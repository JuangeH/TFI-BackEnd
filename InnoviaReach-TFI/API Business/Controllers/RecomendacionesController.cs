using API_Business.Response;
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
        private readonly IMapper _mapper;

        public RecomendacionesController(
            IRecomendacionesService recomendacionesService, IMapper mapper)
        {
            _recomendacionesService = recomendacionesService;
            _mapper = mapper;
        }

        [HttpGet("ObtenerRecomendacionesForoVisitado")]
        public async Task<IActionResult> ObtenerRecomendacionesForoVisitado(string user_id)
        {
            try
            {
                var result = await _recomendacionesService.RecomendacionesPorVisitas(user_id);
                var resultado = _mapper.Map<List<ForoResponse>>(result);

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
