using Api.Controllers;
using Api.Request;
using AutoMapper;
using Core.Contracts.Data;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transversal.Helpers.JWT;
using static System.Net.WebRequestMethods;

namespace _2._API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class VideojuegoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<VideojuegoController> _logger;
        private string ApiBaseURL = "https://localhost:44309/";

        public VideojuegoController(
            IMapper mapper,
            ILogger<VideojuegoController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("RegistrarInformacion")]
        [Authorize]
        public async Task<IActionResult> RegistrarInformacion(SteamInfoRequest steamInfoRequest)
        {
            string userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string URL = ApiBaseURL + $"Videojuego/RegistrarInformacion/{userid}";
            var GenericApiResponse = await RequestHelper.PostRequest<bool, SteamInfoRequest>(URL, steamInfoRequest);
            return Ok(GenericApiResponse);
        }
    }
}
