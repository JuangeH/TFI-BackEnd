using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IRecomendacionesService
    {
        public Task<List<ForoModel>> RecomendacionesPorVisitas(string User_ID);
    }
}
