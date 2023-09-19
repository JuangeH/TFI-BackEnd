using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IGeneroService : IGenericService<GeneroModel>
    {
        public Task<List<GeneroModel>> ObtenerGeneros();
    }
}
