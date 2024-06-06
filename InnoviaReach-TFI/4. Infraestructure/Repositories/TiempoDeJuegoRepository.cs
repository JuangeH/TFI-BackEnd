using Core.Contracts.Repositories;
using Core.Domain.Models;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Infraestructure.Repositories
{
    public class TiempoDeJuegoRepository : GenericRepository<TiempoDeJuegoModel>, ITiempoDeJuegoRepository
    {
        public TiempoDeJuegoRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
