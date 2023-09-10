using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models;

namespace _3._Core.Domain.Entities
{
    public class Videojuego
    {
        public List<EstiloModel> Estilos { get; set; }
        public List<Genero> Generos { get; set; }
        public string Nombre { get; set; }
        public List<Novedad> Novedades { get; set; }
        public int Plataforma_ID { get; set; }
        public List<ReseñaModel> Reseñas { get; set; }
        public List<ValoracionModel> Valoraciones { get; set; }
        public int Videojuego_ID { get; set; }
    }
}
