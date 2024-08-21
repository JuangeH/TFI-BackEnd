using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ForoModel
    {
        public ForoModel()
        {
            foroUsuarioVisitaModels = new HashSet<ForoUsuarioVisitaModel>();
            foroUsuarioFavoritoModels = new HashSet<ForoUsuarioFavoritoModel>();
            comentarioModels = new HashSet<ComentarioModel>();
        }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int Foro_ID { get; set; }
        public string User_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public DateTime FechaCreado { get; set; }
        public bool Activo { get; set; }
        public VideojuegoModel videojuego { get; set; }
        public Users usuario { get; set; }
        public ICollection<ForoUsuarioVisitaModel> foroUsuarioVisitaModels { get; set; }
        public ICollection<ForoUsuarioFavoritoModel> foroUsuarioFavoritoModels { get; set; }
        public ICollection<ComentarioModel> comentarioModels { get; set; }
    }
}
