using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ValoracionModel
    {
        public int Puntuacion { get; set; }
        public int Valoracion_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel Videojuego { get; set; } 
    }
}
