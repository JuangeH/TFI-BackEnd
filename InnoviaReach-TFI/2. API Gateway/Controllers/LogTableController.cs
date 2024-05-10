using _2._API.Response;
using _3._Core.Services;
using AutoMapper;
using Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace _2._API.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class LogTableController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LogTableController> _logger;
        private readonly ILogService _logService;

        public LogTableController(
            IMapper mapper,
            ILogger<LogTableController> logger,
            ILogService logService)
        {
            _mapper = mapper;
            _logger = logger;
            _logService = logService;
        }

        [HttpGet("ObtenerLogs")]
        public async Task<IActionResult> ObtenerLogs()
        {
            try
            {
                var resultado = await _logService.ObtenerLogs();
                var response = _mapper.Map<List<LogTableResponse>>(resultado);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while obtaining logs.");
                return BadRequest(ex.Message);
            }
        }
    }
}

