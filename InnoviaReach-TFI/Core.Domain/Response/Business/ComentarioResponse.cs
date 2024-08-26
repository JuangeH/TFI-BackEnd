using Core.Domain.ApplicationModels;
using Core.Domain.Models;

namespace API_Business.Response
{
    public class ComentarioResponse
    {
        public ComentarioResponse()
        {
            Respuestas = new HashSet<ComentarioResponse>();
        }

        public int Codigo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Contenido { get; set; }
        public string Creador { get; set; }
        public int? ComentarioPadre_Codigo { get; set; }
        public double PromedioPuntaje { get; set; }
        public int CantidadVotos { get; set; }

        public ICollection<ComentarioResponse> Respuestas;
    }
}
