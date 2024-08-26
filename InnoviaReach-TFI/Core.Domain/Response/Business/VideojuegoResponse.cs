using Core.Domain.Models;

namespace API_Business.Response
{
    public class VideojuegoResponse
    {
        public VideojuegoResponse()
        {
            VideojuegoEstilo = new List<VideojuegoEstiloResponse>();
            VideojuegoGenero = new List<VideojuegoGeneroResponse>();
        }

        public List<VideojuegoEstiloResponse> VideojuegoEstilo { get; set; }
        public List<VideojuegoGeneroResponse> VideojuegoGenero { get; set; }
        public string Nombre { get; set; }
        public PlataformaResponse Plataforma { get; set; }



    }
}
