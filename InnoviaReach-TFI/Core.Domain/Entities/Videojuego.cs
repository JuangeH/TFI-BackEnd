using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Videojuego
    {
        public List<EstiloDeJuego> Estilos { get; set; }
        public List<Genero> Generos { get; set; }
        public string Nombre { get; set; }
        public List<Novedad> Novedades { get; set; }
        public int Plataforma_ID { get; set; }
        public List<Reseña> Reseñas { get; set; }
        public List<Valoracion> Valoraciones { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
