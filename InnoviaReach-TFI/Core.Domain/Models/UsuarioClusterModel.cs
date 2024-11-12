using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class UsuarioClusterModel
    {
        public string UserId { get; set; }

        [NotMapped]
        public Dictionary<string, int> GameGenres { get; set; }

        [NotMapped]
        public Dictionary<string, int> GameTags { get; set; }

        [NotMapped]
        public List<string> GameHistory { get; set; }

        public int ClusterID { get; set; }

        // Vectores transformados para ML.NET
        [VectorType(5)] public float[] GenresVector { get; set; } // Tamaño fijo basado en el vocabulario
        [VectorType(5)] public float[] TagsVector { get; set; }

    }


}

