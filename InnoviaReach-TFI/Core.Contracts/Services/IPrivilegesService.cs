using Core.Domain.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IPrivilegesService : IGenericService<Privileges>
    {
        public Task<bool> CreatePrivilegeAsync(Privileges privileges);
        public Task<bool> DeletePrivilegeAsync(string id);
        public Task<Privileges> UpdatePrivilegeAsync(Privileges privileges);
        public Task<List<Privileges>> GetPrivilegesAsync();
        public Task<Privileges> GetPrivilegeByIdAsync(string id);
    }
}
