using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class UsuarioClusterDataModel
    {
        public string UserId { get; set; }
        [VectorType] public float[] GenresVector { get; set; }
        [VectorType] public float[] TagsVector { get; set; }
    }
}
