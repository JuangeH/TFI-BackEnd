using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class TiendaModel
    {
        public TiendaModel()
        {
            videojuegoTiendaModels = new HashSet<VideojuegoTiendaModel>();
        }
        public ICollection<VideojuegoTiendaModel> videojuegoTiendaModels { get; set; }
        public int Tienda_ID { get; set; }
        public int StoreRawgId { get; set; }
        public string Nombre { get; set; }
        public string Slug { get; set; }
        public string Dominio { get; set; }
    }
}
