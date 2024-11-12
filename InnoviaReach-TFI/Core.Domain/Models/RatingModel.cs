using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class RatingModel
    {
        public int Rating_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public string Titulo { get; set; }
        public int CantidadVotos { get; set; }
        public float Porcentaje { get; set; }
        public VideojuegoModel Videojuego { get; set; }
    }
}
