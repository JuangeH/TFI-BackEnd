using _3._Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoModel
    {
        public int Videojuego_ID { get; set; }
        public string Nombre { get; set; }
        public int Plataforma_ID { get; set; }
        //public List<EstiloDeJuego> Estilos { get; set; }
        //public List<Genero> Generos { get; set; }
        //public List<Novedad> Novedades { get; set; }
        //public List<Reseña> Reseñas { get; set; }
        //public List<Valoracion> Valoraciones { get; set; }
        public PlataformaModel Plataforma { get; set; }
    }
}
