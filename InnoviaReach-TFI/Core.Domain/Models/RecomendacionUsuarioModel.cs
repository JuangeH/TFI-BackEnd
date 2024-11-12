using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class RecomendacionUsuarioModel
    {
        public int RecomendacionId { get; set; }
        public string UserId { get; set; } // ID del usuario que recibe la recomendación
        public int VideojuegoRecomendadoId { get; set; } // ID del videojuego recomendado
        public int Frecuencia { get; set; } // Frecuencia en el historial de usuarios similares
        public string TipoRecomendacion { get; set; } = "Colaborativa"; // Tipo de recomendación
        public DateTime FechaRecomendacion { get; set; } = DateTime.UtcNow;
        public Users usuario { get; set; }
        public VideojuegoModel videojuego { get; set; }
    }
}
