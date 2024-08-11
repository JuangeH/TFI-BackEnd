using AutoMapper;
using Core.Domain.Helper;
using Core.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Gateway.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class ForoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ForoController> _logger;
        private string ApiBaseURL = "https://localhost:44309/";

        public ForoController(
            IMapper mapper,
            ILogger<ForoController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("ObtenerForosGenerales")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerForosGenerales()
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string URL = ApiBaseURL + $"Foro/ObtenerForosGenerales/{userid}";
            var GenericApiResponse = await RequestHelper.GetRequest<ForoModel>(URL);
            return Ok(GenericApiResponse);
        }
    }
}
