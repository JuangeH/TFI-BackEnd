using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Foro
    {
        public List<Comentario> Comentarios { get; set; }
        public string Descripcion { get; set; }
        public int Foro_ID { get; set; }
        public bool Tipo { get; set; }
        public string Titulo { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
