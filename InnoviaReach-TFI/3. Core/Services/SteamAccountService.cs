using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3._Core.Services
{
    public class SteamAccountService : GenericService<SteamAccountModel>, ISteamAccountService
    {
        public SteamAccountService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<ISteamAccountRepository>())
        {

        }

        public async Task RegistrarUsuario(SteamAccountModel account)
        {
            try
            {
                var steamAccount = (await _repository.Get(x => x.User_ID == account.User_ID && x.steamid == account.steamid)).FirstOrDefault();

                if (steamAccount is null)
                {
                    await _repository.Insert(account);

                    await _unitOfWork.SaveChangesAsync();
                }

                

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<SteamAccountModel> ValidarSteamAccount(string User_ID)
        {
            try
            {
                return (await _repository.GetOne(x => x.User_ID == User_ID));

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
