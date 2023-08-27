using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IUsersPrivilegesService : IGenericService<UsersPrivileges>
    {
        public Task<bool> AssignPrivilegesToUser(string userId, List<string> privileges);
        public Task<IList<string>> GetUserPrivileges(string userId);
    }
}
