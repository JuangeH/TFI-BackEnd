using Newtonsoft.Json;
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
        public int Genero_ID { get; set; }
        public int GenreRawgID { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
    }
}
