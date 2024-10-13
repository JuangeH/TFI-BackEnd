using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AdquisicionModel
    {
        public int Adquisicion_id { get; set; }
        public string User_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public Users usuario { get; set; }
        public VideojuegoModel videojuego { get; set; }
        public int TiempoJuego { get; set; }
        public int? CantidadLogros { get; set; }
        public int? TiempoJuegoReciente { get; set; }
    }
}
