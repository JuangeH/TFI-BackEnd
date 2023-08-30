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
    public class PlataformaRepository : GenericRepository<PlataformaModel>, IPlataformaRepository
    {
        public PlataformaRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
