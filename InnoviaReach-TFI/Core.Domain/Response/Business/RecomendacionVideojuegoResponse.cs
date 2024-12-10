using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Response.Business
{
    public class RecomendacionVideojuegoResponse
    {
        public string VideojuegoReferencia { get; set; }
        public string VideojuegoRecomendado { get; set; }
        public string VideojuegoRecomendadoImagen { get; set; }
        public string TipoRecomendacion { get; set; }
    }
}
