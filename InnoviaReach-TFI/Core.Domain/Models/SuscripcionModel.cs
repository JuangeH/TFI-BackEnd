using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class SuscripcionModel
    {
        public SuscripcionModel()
        {
            suscripcionUsrdModels = new HashSet<SuscripcionUsuarioModel>();
        }
        public ICollection<SuscripcionUsuarioModel> suscripcionUsrdModels { get; set; }

        public string Descripcion { get; set; }
        public int Suscripcion_ID { get; set; }
    }
}
