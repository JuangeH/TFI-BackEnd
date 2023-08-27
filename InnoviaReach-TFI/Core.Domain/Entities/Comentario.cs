using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Comentario
    {
        public int Comentario_ID { get; set; }
        public string Descripcion { get; set; }
        public int Foro_ID { get; set; }
        public int User_ID { get; set; }

    }
}
