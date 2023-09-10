using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class GeneroModel
    {
        public GeneroModel()
        {
            videojuegoGeneroModels = new HashSet<VideojuegoGeneroModel>();
        }
        public ICollection<VideojuegoGeneroModel> videojuegoGeneroModels { get; set; }
        public string Descripcion { get; set; }
        public int Genero_ID { get; set; }
    }
}
