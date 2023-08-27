using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Domain.Entities
{
    public class MetodoDePago
    {
        public int Cod_Postal { get; set; }
        public int Cod_Verificador { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public int Medio_ID { get; set; }
        public double Numero { get; set; }
        public int TipoPago_ID { get; set; }
        public string User_ID { get; set; }
    }
}
