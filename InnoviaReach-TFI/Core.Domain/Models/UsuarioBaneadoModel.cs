using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class UsuarioBaneadoModel
    {
        public int Baneo_ID { get; set; }
        public string User_ID { get; set; }
        public string UserAdmin_ID { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaDeBaneo { get; set; }
        public Users usuarioBaneado { get; set; }
        public Users usuarioAdmin { get; set; }
    }
}
