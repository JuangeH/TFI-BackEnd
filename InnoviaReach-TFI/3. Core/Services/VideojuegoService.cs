using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transversal.EmailService.Configurations;
using Transversal.EmailService.Factory;
using Transversal.Helpers;

namespace _3._Core.Services
{
    public class VideojuegoService : GenericService<VideojuegoModel>, IVideojuegoService
    {
        public VideojuegoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IVideojuegoRepository>())
        {

        }

        public async Task<List<VideojuegoModel>> ObtenerVideojuegosYPlataformas()
        {
            try
            {
                return (await _repository.Get(x => x.Nombre != "", includeProperties: "Plataforma")).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
