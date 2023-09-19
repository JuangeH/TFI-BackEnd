using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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
        private readonly IVideojuegoEstiloRepository _videojuegoEstiloRepo;
        private readonly IVideojuegoGeneroRepository _videojuegoGeneroRepo;
        private readonly IEstiloRepository _EstiloRepo;
        private readonly IGeneroRepository _GeneroRepo;
        private VideojuegoEstiloModel _videojuegoEstiloModel;
        private VideojuegoGeneroModel _videojuegoGeneroModel;

        public VideojuegoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.GetRepository<IVideojuegoRepository>())
        {
            _videojuegoEstiloRepo = unitOfWork.GetRepository<IVideojuegoEstiloRepository>();
            _videojuegoGeneroRepo = unitOfWork.GetRepository<IVideojuegoGeneroRepository>();
            _EstiloRepo = unitOfWork.GetRepository<IEstiloRepository>();
            _GeneroRepo = unitOfWork.GetRepository<IGeneroRepository>();
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

        public async Task RegistrarVideojuego(VideojuegoModel videojuego, JArray categoriesArray, JArray genresArray)
        {
            try
            {
                await _repository.Insert(videojuego);

                _unitOfWork.SaveChanges();

                var estilos = await _EstiloRepo.Get(x => x.Estilo_ID.ToString() != "");
                var generos = await _GeneroRepo.Get(x => x.Genero_ID.ToString() != "");

                foreach (var item in categoriesArray.ToList())
                {
                    foreach (var item2 in estilos)
                    {
                        if (item["description"].ToString() == item2.Descripcion)
                        {
                            _videojuegoEstiloModel = new VideojuegoEstiloModel();

                            _videojuegoEstiloModel.Videojuego_ID = videojuego.Videojuego_ID;
                            _videojuegoEstiloModel.Estilo_ID = item2.Estilo_ID;


                            await _videojuegoEstiloRepo.Insert(_videojuegoEstiloModel);
                        }
                    }
                }
                foreach (var item in genresArray.ToList())
                {
                    foreach (var item2 in generos)
                    {
                        if (item["description"].ToString() == item2.Descripcion)
                        {
                            _videojuegoGeneroModel = new VideojuegoGeneroModel();

                            _videojuegoGeneroModel.Videojuego_ID = videojuego.Videojuego_ID;
                            _videojuegoGeneroModel.Genero_ID = item2.Genero_ID;


                            await _videojuegoGeneroRepo.Insert(_videojuegoGeneroModel);
                        }
                    }
                }

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            
        }
    }
}
