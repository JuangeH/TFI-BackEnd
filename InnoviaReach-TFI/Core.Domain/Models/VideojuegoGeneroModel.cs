using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoGeneroModel
    {
        public int ID { get; set; }
        public int Genero_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel? videojuego { get; set; }
        public GeneroModel? generoModel { get; set; }
    }
}
