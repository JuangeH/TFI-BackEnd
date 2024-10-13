using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface ISteamAccountService : IGenericService<SteamAccountModel>
    {
        public Task<SteamAccountModel> ValidarSteamAccount(string User_ID);
        public Task RegistrarUsuario(SteamAccountModel account);
    }
}
