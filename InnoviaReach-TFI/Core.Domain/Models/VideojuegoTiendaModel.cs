using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoTiendaModel
    {
        public int ID { get; set; }
        public int Tienda_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel? videojuego { get; set; }
        public TiendaModel? tiendaModel { get; set; }
    }
}
