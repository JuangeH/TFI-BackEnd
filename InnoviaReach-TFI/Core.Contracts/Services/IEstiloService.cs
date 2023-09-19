using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IEstiloService : IGenericService<EstiloModel>
    {
        public Task<List<EstiloModel>> ObtenerEstilos();
    }
}
