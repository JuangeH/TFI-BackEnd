using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class TipoPagoModel
    {
        public TipoPagoModel()
        {
            _mediosPagoModel = new HashSet<MedioDePagoModel>();
        }

        public string Descripcion { get; set; }
        public int TipoPago_ID { get; set; }
        public ICollection<MedioDePagoModel> _mediosPagoModel { get; set; }
    }
}
