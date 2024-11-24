using Core.Domain.Models.Nueva_Base;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class VideojuegoModel
    {
        public VideojuegoModel()
        {
            novedadModels = new HashSet<NovedadModel>();
            trofeosModel = new HashSet<TrofeoModel>();
            adquisicionesModel = new HashSet<AdquisicionModel>();
            tiempoDeJuegoModel = new HashSet<TiempoDeJuegoModel>();
            valoracionModel = new HashSet<ValoracionModel>();
            reseñaModel = new HashSet<ReseñaModel>();
            videojuegoEstiloModels = new HashSet<VideojuegoEstiloModel>();
            videojuegoInteresModels = new HashSet<VideojuegoInteresModel>();
            foroModels = new HashSet<ForoModel>();
            ratingModels = new HashSet<RatingModel>();
            videojuegoGeneroModels = new HashSet<VideojuegoGeneroModel>();
            videojuegoTiendaModels = new HashSet<VideojuegoTiendaModel>();
            videojuegoTagModels = new HashSet<VideojuegoTagModel>();
            usuarioVisitaModels = new HashSet<UsuarioVisitaModel>();
            recomendacionUsuarioModels = new HashSet<RecomendacionUsuarioModel>();
            recomendacionVideojuegoRecModels = new HashSet<RecomendacionVideojuegoModel>();
        }

        public int Videojuego_ID { get; set; }
        public int AppRawgId { get; set; }
        public string Slug { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? Imagen { get; set; }
        public float Rating { get; set; }
        public int? Metacritic { get; set; }
        public int? ClusterID { get; set; }
        public string? CaracteristicasVector { get; set; }
        public string Descripcion { get; set; }

        //public List<EstiloDeJuego> Estilos { get; set; }
        //public List<Genero> Generos { get; set; }
        //public List<Novedad> Novedades { get; set; }
        //public List<Reseña> Reseñas { get; set; }
        //public List<Valoracion> Valoraciones { get; set; }
        //public ICollection<PlataformaModel> plataformaModels { get; set; }
        public ICollection<VideojuegoPlataformaModel> videojuegoPlataformaModels { get; set; }
        public ICollection<NovedadModel> novedadModels { get; set; }
        public ICollection<TrofeoModel> trofeosModel { get; set; }
        public ICollection<AdquisicionModel> adquisicionesModel { get; set; }
        public ICollection<TiempoDeJuegoModel> tiempoDeJuegoModel { get; set; }
        public ICollection<ValoracionModel> valoracionModel { get; set; }
        public ICollection<ReseñaModel> reseñaModel { get; set; }
        public ICollection<VideojuegoEstiloModel> videojuegoEstiloModels { get; set; }
        public ICollection<VideojuegoInteresModel> videojuegoInteresModels { get; set; }
        public ICollection<ForoModel> foroModels { get; set; }
        public ICollection<RatingModel> ratingModels { get; set; }
        public ICollection<VideojuegoGeneroModel> videojuegoGeneroModels { get; set; }
        public ICollection<VideojuegoTiendaModel> videojuegoTiendaModels { get; set; }
        public ICollection<VideojuegoTagModel> videojuegoTagModels { get; set; }
        public ICollection<UsuarioVisitaModel> usuarioVisitaModels { get; set; }
        public ICollection<RecomendacionUsuarioModel> recomendacionUsuarioModels { get; set; }
        public ICollection<RecomendacionVideojuegoModel> recomendacionVideojuegoRecModels { get; set; }
        public ICollection<RecomendacionVideojuegoModel> recomendacionVideojuegoRefModels { get; set; }
    }
}
