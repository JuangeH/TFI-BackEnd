using System;
using System.Collections.Generic;
using System.Text;
using Core.Contracts.Repositories;
using Core.Domain.ApplicationModels;

namespace Infrastructure.Data.Repositories
{
    public class PrivilegesRepository : GenericRepository<Privileges>, IPrivilegesRepository
    {
        public PrivilegesRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
    }
}
