using Core.Contracts.Repositories;
using Core.Domain.ApplicationModels;
using Infrastructure.Data.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Models;

namespace _4._Infraestructure.Repositories
{
    public class VideojuegoRepository : GenericRepository<VideojuegoModel>, IVideojuegoRepository
    {
        public VideojuegoRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
