using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.Extensions;

namespace _3._Core.Services
{
    public class ForoUsuarioVisitaService : GenericService<ForoUsuarioVisitaModel>, IForoUsuarioVisitaService
    {
        private readonly ILogger<ComentarioService> _logger;
        public ForoUsuarioVisitaService(IUnitOfWork unitOfWork, ILogger<ComentarioService> logger)
            : base(unitOfWork, unitOfWork.GetRepository<IForoUsuarioVisitaRepository>())
        {
            _logger = logger;
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
                    _logger.LogBusiness($"El usuario [{User_ID}] visitó el foro [{Foro_ID}]");

                }
            }
            catch (Exception)
            {
                throw new Exception($"Error al registrar visita del usuario {User_ID} sobre el foro {Foro_ID}");
            }
        }

    }
}