using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IRecomendacionService : IGenericService<RecomendacionModel>
    {
        public Task<List<ForoModel>> RecomendacionesPorVisitas(string User_ID);
        public Task CrearClusters(List<VideojuegoClusterModel> juegos);
        public float CalcularSimilitudCoseno(float[] vectorA, float[] vectorB);
        public Task GenerarRecomendaciones(VideojuegoModel juegoReferencia, string Usuario_ID);
        public Task CrearClustersUsuarios();
        public Task<List<string>> GenerarRecomendacionesColaborativas(string UserID);
    }
}
