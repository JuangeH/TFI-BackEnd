using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Novedad
    {
        public string Descripcion { get; set; }
        public int Novedad_ID { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
