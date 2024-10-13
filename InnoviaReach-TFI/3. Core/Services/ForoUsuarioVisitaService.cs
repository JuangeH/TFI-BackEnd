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

namespace _3._Core.Services
{
    public class ForoUsuarioVisitaService : GenericService<ForoUsuarioVisitaModel>, IForoUsuarioVisitaService
    {
        public ForoUsuarioVisitaService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IForoUsuarioVisitaRepository>())
        {

        }

        public async Task RegistrarVisita(string User_ID, int Foro_ID)
        {
            try
            {
                var visitaModel = (await _repository.Get(x => x.Foro_ID == Foro_ID && x.User_ID == User_ID)).FirstOrDefault();

                bool isForoOwner = (await _unitOfWork.GetRepository<IForoRepository>().Get(x => x.User_ID == User_ID)).Any();

                if (visitaModel is null && !isForoOwner)
                {
                    ForoUsuarioVisitaModel visita = new ForoUsuarioVisitaModel();

                    visita.Foro_ID = Foro_ID;

                    visita.User_ID = User_ID;

                    await _repository.Insert(visita);

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}