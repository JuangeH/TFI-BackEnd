using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class PlataformaModel
    {
        public PlataformaModel()
        {
            videojuegoPlataformaModels = new HashSet<VideojuegoPlataformaModel>();
        }
        public int Plataforma_ID { get; set; }
        public int PlatformRawgID { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public ICollection<VideojuegoPlataformaModel> videojuegoPlataformaModels { get; set; }
    }
}
