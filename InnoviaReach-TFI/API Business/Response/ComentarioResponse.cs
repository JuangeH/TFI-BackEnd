using Core.Domain.ApplicationModels;
using Core.Domain.Models;

namespace API_Business.Response
{
    public class ComentarioResponse
    {
        public DateTime FechaCreacion { get; set; }
        public string Contenido { get; set; }
        public string Creador { get; set; }
        public int? ComentarioPadre_Codigo { get; set; }
    }
}
