using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Request.Business
{
    public class ComentarioRequest
    {
        public DateTime FechaCreacion { get; set; }
        public string Contenido { get; set; }
        public int? ComentarioPadre_Codigo { get; set; }
        public int Foro_Codigo { get; set; }
        public string User_ID { get; set; }
    }
}
