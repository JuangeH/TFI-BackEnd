using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ForoUsuarioModel
    {
        public int ID { get; set; }
        public string User_ID { get; set; }
        public Users usuario { get; set; }
        public int Foro_ID { get; set; }
        public ForoModel foro { get; set; }
        public bool Tipo { get; set; }

    }
}
