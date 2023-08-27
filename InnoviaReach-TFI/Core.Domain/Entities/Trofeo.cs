using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Trofeo
    {
        public int Trofeo_ID { get; set; }
        public string Descripcion { get; set; }
        public string User_ID { get; set; }
        public int Videojuego_ID { get; set; }

    }
}
