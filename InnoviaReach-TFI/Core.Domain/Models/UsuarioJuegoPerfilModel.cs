using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class UsuarioJuegoPerfilModel
    {
        public int Perfil_ID { get; set; } 
        public string User_ID { get; set; } 
        public int? ClusterID { get; set; }
        public string? TipoRecomendacion { get; set; }
        public string? GameGenresJson { get; set; }
        public string? GameTagsJson { get; set; }
        public string? GameHistoryJson { get; set; }
        public Users User { get; set; } 
    }
}
