using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoEstiloModel
    {
        public int ID { get; set; }
        public int Estilo_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel? videojuego { get; set; }
        public EstiloModel? estiloModel { get; set; }
    }
}
