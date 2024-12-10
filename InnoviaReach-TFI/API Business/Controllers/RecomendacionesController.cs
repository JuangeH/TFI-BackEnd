using API_Business.Response;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Response.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API_Business.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class RecomendacionesController : ControllerBase
    {
        private readonly IRecomendacionService _recomendacionesService;
        private readonly IVideojuegoService _videojuegoService;
        private readonly IMapper _mapper;
        private readonly ILogger<RecomendacionesController> _logger;    

        public RecomendacionesController(
            IRecomendacionService recomendacionesService, IMapper mapper, IVideojuegoService videojuegoService, ILogger<RecomendacionesController> logger)
        {
            _recomendacionesService = recomendacionesService;
            _mapper = mapper;
            _videojuegoService = videojuegoService;
            _logger = logger;
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

        [HttpGet("ObtenerForosRecomendadosVisita")]
        public async Task<IActionResult> ObtenerForosRecomendadosVisita(string Usuario_ID)
        {
            try
            {
                var recomendaciones = _mapper.Map<List<ForoResponse>>(await _recomendacionesService.ObtenerForosRecPorVisitas(Usuario_ID));
                return Ok(recomendaciones);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ObtenerForosRecomendadosForosFav")]
        public async Task<IActionResult> ObtenerForosRecomendadosForosFav(string Usuario_ID)
        {
            try
            {
                var recomendaciones = _mapper.Map<List<ForoResponse>>(await _recomendacionesService.ObtenerForosRecPorFavoritos(Usuario_ID));
                return Ok(recomendaciones);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ObtenerForosRecomendadosColab")]
        public async Task<IActionResult> ObtenerForosRecomendadosColab(string Usuario_ID)
        {
            try
            {
                var recomendaciones = _mapper.Map<List<ForoResponse>>(await _recomendacionesService.ObtenerForosRecColaborativos(Usuario_ID));
                return Ok(recomendaciones);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ObtenerRecomendacionesIndividuales")]
        public async Task<IActionResult> ObtenerRecomendacionesIndividuales(string Usuario_ID)
        {
            try
            {
                List<RecomendacionVideojuegoResponse> recomendaciones = new List<RecomendacionVideojuegoResponse>();

                var recVis = _mapper.Map<List<RecomendacionVideojuegoResponse>>(await _recomendacionesService.ObtenerRecomendacionesVisitas(Usuario_ID)); 
                var recForFav = _mapper.Map<List<RecomendacionVideojuegoResponse>>(await _recomendacionesService.ObtenerRecomendacionesForosFav(Usuario_ID));
                recomendaciones.AddRange(recVis);
                recomendaciones.AddRange(recForFav);

                return Ok(recomendaciones);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ObtenerRecomendacionesColaborativas")]
        public async Task<IActionResult> ObtenerRecomendacionesColaborativas(string Usuario_ID)
        {
            try
            {
                List<RecomendacionUsuarioResponse> recomendaciones = new List<RecomendacionUsuarioResponse>();

                var recVis = _mapper.Map<List<RecomendacionUsuarioResponse>>(await _recomendacionesService.ObtenerRecomendacionesColabVisitas(Usuario_ID));
                var recForFav = _mapper.Map<List<RecomendacionUsuarioResponse>>(await _recomendacionesService.ObtenerRecomendacionesColabForosFav(Usuario_ID));
                recomendaciones.AddRange(recVis);
                recomendaciones.AddRange(recForFav);

                return Ok(recomendaciones);

            }
            catch (Exception ex)
            {
                throw;;
            }
        }
    }
}
