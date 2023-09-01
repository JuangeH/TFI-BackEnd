using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class NovedadModel
    {
        public string Descripcion { get; set; }
        public int Novedad_ID { get; set; }
        public VideojuegoModel Videojuego { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
