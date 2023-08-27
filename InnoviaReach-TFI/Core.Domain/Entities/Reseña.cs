using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Reseña
    {
        public string Descripción { get; set; }
        public int Reseña_ID { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
