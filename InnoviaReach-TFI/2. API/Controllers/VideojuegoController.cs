using Api.Controllers;
using Api.Request;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transversal.Extensions;

namespace _2._API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VideojuegoController> _logger;
        private readonly IVideojuegoService _videojuegoService;
        private readonly IPlataformaService _plataformaService;

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger,
            IVideojuegoService videojuegoService,
            IPlataformaService plataformaService)
        {
            _mapper = mapper;
            _logger = logger;
            _videojuegoService = videojuegoService;
            _plataformaService = plataformaService;
        }

        [HttpGet("ObtenerVideojuegos")]
        public async Task<IActionResult> ObtenerVideojuegos()
        {
            try
            {
               var resultado =  await _videojuegoService.ObtenerVideojuegosYPlataformas();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining videogames.");
                return BadRequest(ex.Message);
            }
        }
    }
}
