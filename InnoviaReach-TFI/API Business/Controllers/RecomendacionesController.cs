using API_Business.Response;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.Models;
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

        [HttpGet("ObtenerRecomendacionesVisita")]
        public async Task<IActionResult> ObtenerRecomendacionesVisita(string userID)
        {
            try
            {
                //appid = 989185;

                ////var videojuegos = _mapper.Map<List<VideojuegoClusterModel>>(await _videojuegoService.ObtenerVideojuegos());
                //var videojuego = await _videojuegoService.ObtenerVideojuego(appid);
                _logger.LogInformation("Hola Mundo");


                //await _recomendacionesService.GenerarRecomendacionesColaborativas(userID);

                //await _recomendacionesService.GenerarRecomendaciones(videojuego);

                return Ok();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //[HttpGet("ObtenerRecomendacionesHistorialVisita")]
        //public async Task<IActionResult> ObtenerRecomendacionesHistorialVisita()
        //{
        //    try
        //    {
        //        appid = 3498;

        //        var videojuegos = _mapper.Map<List<VideojuegoClusterModel>>(await _videojuegoService.ObtenerVideojuegos());
        //        var videojuego = videojuegos.FirstOrDefault(v => v.AppRawgId == appid);

        //        _recomendacionesService.CrearClusters(videojuegos);

        //        var result = _recomendacionesService.GenerarRecomendaciones(videojuegos, videojuego);
        //        var resultado = _mapper.Map<List<VideojuegoModel>>(result);

        //        return Ok(resultado);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
