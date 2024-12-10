using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Request.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class UsuarioBaneadoService : GenericService<UsuarioBaneadoModel>, IUsuarioBaneadoService
    {
        private readonly IUsersRepository _usersRepository;

        public UsuarioBaneadoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IUsuarioBaneadoRepository>())
        {
            _usersRepository = unitOfWork.GetRepository<IUsersRepository>();
        }

        public async Task BanearUsuario(UsuarioBaneadoRequest usuarioBaneadoRequest)
        {
            try
            {
                UsuarioBaneadoModel model = new UsuarioBaneadoModel();
                var user = (await _usersRepository.Get(x => x.UserName == usuarioBaneadoRequest.UserName)).FirstOrDefault();
                user.CommunityBanned = true;
                model.User_ID = user.Id;
                model.UserAdmin_ID = usuarioBaneadoRequest.UserAdmin_ID;
                model.Motivo = usuarioBaneadoRequest.Motivo;
                model.FechaDeBaneo = usuarioBaneadoRequest.FechaDeBaneo;

                await _repository.Insert(model);
                await _usersRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al banear el usaurio {usuarioBaneadoRequest}");
            }

            
        }

        public async Task DesbanearUsuario(string UserName)
        {
            try
            {
                var user = (await _usersRepository.Get(x => x.UserName == UserName)).FirstOrDefault();
                user.CommunityBanned = false;

                await _usersRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception )
            {
                throw new Exception($"Error al desbanear el usaurio {UserName}");
            }
            
        }
    }
}
