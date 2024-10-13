using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Repositories
{
    public interface ISteamAccountRepository : IGenericRepository<SteamAccountModel>
    {
    }
}
