using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public double DNI { get; set; }
        public bool Estado { get; set; }
        public string Idioma { get; set; }
        public string Mail { get; set; }
        public int Notificacion_ID { get; set; }
        public string User_ID { get; set; }
        public string VEstilo_preferido { get; set; }
        public string VGenero_preferido { get; set; }

    }
}
