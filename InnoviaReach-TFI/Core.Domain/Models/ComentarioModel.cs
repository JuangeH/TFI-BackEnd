using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ComentarioModel
    {
        public ComentarioModel()
        {
            puntuacionModels = new HashSet<PuntuacionModel>();
            comentarioModels = new HashSet<ComentarioModel>();
        }
        public DateTime FechaCreacion { get; set; }
        public int Comentario_ID { get; set; }
        public string Contenido { get; set; }
        public int Foro_ID { get; set; }
        public ForoModel foro { get; set; }
        public string User_ID { get; set; }
        public Users usuario { get; set; }
        public int? ComentarioPadre_ID { get; set; }
        public ComentarioModel comentarioPadre { get; set; }

        public ICollection<PuntuacionModel> puntuacionModels { get; set; }
        public ICollection<ComentarioModel> comentarioModels { get; set; }

    }
}
