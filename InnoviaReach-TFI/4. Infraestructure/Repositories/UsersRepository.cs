using Core.Contracts.Repositories;
using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Repositories
{
    public class UsersRepository:GenericRepository<Users>,IUsersRepository
    {
        public UsersRepository(ApplicationDbContext applicationDbContext) 
            : base(applicationDbContext)
        {
            
        }
    }
}
