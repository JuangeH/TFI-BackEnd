using API_Business.Response;
using AutoMapper;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
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

        public ForoController(
            IMapper mapper,
            ILogger<ForoController> logger,
            IForoService foroService)
        {
            _mapper = mapper;
            _logger = logger;
            _foroService = foroService;
        }

        [HttpGet("ObtenerForosGenerales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerForosGenerales()
        {
            try
            {
                var result = await _foroService.ObtenerForosGenerales();
                var response = _mapper.Map<List<ForoResponse>>(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
