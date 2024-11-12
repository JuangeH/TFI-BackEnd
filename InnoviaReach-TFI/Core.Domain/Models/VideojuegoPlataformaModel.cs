using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoPlataformaModel
    {
        public int ID { get; set; }
        public int Plataforma_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel? videojuego { get; set; }
        public PlataformaModel? plataformaModel { get; set; }
    }
}
