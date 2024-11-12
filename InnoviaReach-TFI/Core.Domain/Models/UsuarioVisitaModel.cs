using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class UsuarioVisitaModel
    {
        public int Visita_ID { get; set; }
        public string User_ID { get; set; }
        public Users Usuario { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel Videojuego { get; set; }
        public DateTime Fecha { get; set; }

    }
}
