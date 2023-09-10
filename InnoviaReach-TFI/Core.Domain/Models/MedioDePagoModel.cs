using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class MedioDePagoModel
    {
        public int Cod_Postal { get; set; }
        public int Cod_Verificador { get; set; }
        public string Direccion { get; set; }
        public bool Estado { get; set; }
        public int Medio_ID { get; set; }
        public int Numero { get; set; }
        public int TipoPago_ID { get; set; }
        public string User_ID { get; set; }
        public Users usuario { get; set; }
        public TipoPagoModel tipoPago { get; set; }
    }
}
