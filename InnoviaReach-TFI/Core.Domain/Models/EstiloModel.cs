using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class EstiloModel
    {
        public EstiloModel()
        {
            videojuegoEstiloModels = new HashSet<VideojuegoEstiloModel>();
        }
        public ICollection<VideojuegoEstiloModel> videojuegoEstiloModels { get; set; }
        public string Descripcion { get; set; }
        public int Estilo_ID { get; set; }
       


    }
}
