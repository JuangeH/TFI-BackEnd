using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System;

namespace API_Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideojuegoGTController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public VideojuegoGTController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("ObtenerVideojuegos")]
        public async Task<IActionResult> ObtenerVideojuegos()
        {
            var backendBaseUrl = "https://tubackend.com"; // Cambia esto a la URL de tu API BackEnd

            // Configura la solicitud al backend, por ejemplo, establece encabezados de autenticación
            // _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer tu-token");

            var response = await _httpClient.GetAsync($"{backendBaseUrl}/ObtenerVideojuegos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content); // Retorna los datos del backend al cliente
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
 






}
