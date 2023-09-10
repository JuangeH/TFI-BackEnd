using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3._Core.Domain.Entities;

namespace Core.Domain.Models
{
    public class ForoModel
    {
        public ForoModel()
        {
            foroUsuarioModels = new HashSet<ForoUsuarioModel>();
            comentarioModels = new HashSet<ComentarioModel>();
        }
        public string Descripcion { get; set; }
        public int Foro_ID { get; set; }
        public int Videojuego_ID { get; set; }
        public VideojuegoModel videojuego { get; set; }
        public ICollection<ForoUsuarioModel> foroUsuarioModels { get; set; }
        public ICollection<ComentarioModel> comentarioModels { get; set; }
    }
}
