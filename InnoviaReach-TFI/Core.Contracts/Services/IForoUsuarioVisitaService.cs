using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IForoUsuarioVisitaService : IGenericService<ForoUsuarioVisitaModel>
    {
        public Task RegistrarVisita(string User_ID, int Foro_ID);
    }
}
