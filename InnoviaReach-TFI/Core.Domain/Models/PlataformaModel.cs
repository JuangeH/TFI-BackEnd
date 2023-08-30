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
            videojuegoModels = new HashSet<VideojuegoModel>();
        }
        public string Nombre { get; set; }
        public int Plataforma_ID { get; set; }
        public ICollection<VideojuegoModel> videojuegoModels { get; set; }
    }
}
