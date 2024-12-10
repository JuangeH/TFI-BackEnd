using Core.Domain.Models;
using Core.Domain.Request.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IUsuarioBaneadoService : IGenericService<UsuarioBaneadoModel>
    {
        public Task BanearUsuario(UsuarioBaneadoRequest usuarioBaneadoRequest);
        public Task DesbanearUsuario(string Username);
    }
}
