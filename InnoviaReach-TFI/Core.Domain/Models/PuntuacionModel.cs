using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class PuntuacionModel
    {
        public int Comentario_ID { get; set; }
        public int Puntaje { get; set; }
        public int Puntuacion_ID { get; set; }
        public ComentarioModel comentario { get; set; }
    }
}
