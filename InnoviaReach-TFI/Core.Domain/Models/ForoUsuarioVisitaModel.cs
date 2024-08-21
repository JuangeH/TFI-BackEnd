using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class ForoUsuarioVisitaModel
    {
        public string User_ID { get; set; }
        public Users usuario { get; set; }
        public int Foro_ID { get; set; }
        public ForoModel foro { get; set; }

    }
}
