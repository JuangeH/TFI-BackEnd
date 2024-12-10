using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class RecomendacionVideojuegoModel
    {
        public int RecomendacionId { get; set; }
        public string UserId { get; set; } // ID del usuario que recibe la recomendación
        public int VideojuegoReferenciaId { get; set; } // ID del videojuego de referencia
        public int VideojuegoRecomendadoId { get; set; } // ID del videojuego recomendado
        public float Similitud { get; set; } // Puntuación de similitud
        public string TipoRecomendacion { get; set; } // Tipo de recomendación
        public DateTime FechaRecomendacion { get; set; } = DateTime.UtcNow;
        public Users usuario { get; set; }
        public VideojuegoModel videojuegoReferencia { get; set; }
        public VideojuegoModel videojuegoRecomendado { get; set; }
    }
}
