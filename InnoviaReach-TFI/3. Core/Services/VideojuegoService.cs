using AutoMapper;
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
using System.Net.Http;
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

        public async Task<(List<VideojuegoModel> Videojuegos, int TotalRecords)> ObtenerVideojuegosCatalogo(int pageNumber, int pageSize)
        {
            var query = (await _repository.Get(x => x.Nombre != "")).ToList(); // Aquí deberías ajustar según tu fuente de datos.

            // Contar el total de registros
            var totalRecords = query.Count;

            // Obtener solo los registros para la página actual
            var videojuegos = query
                .Skip((pageNumber - 1) * pageSize) // Saltar los registros anteriores a la página
                .Take(pageSize) // Tomar solo los registros de la página actual
                .ToList();

            return (videojuegos, totalRecords);
        }

        public async Task<List<VideojuegoModel>> ObtenerVideojuegos()
        {
            try
            {
                //return (await _repository.Get(x => x.Nombre != "", includeProperties: "Plataforma, videojuegoEstiloModels, videojuegoEstiloModels.estiloModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel")).ToList();
                return (await _repository.Get(x => x.Nombre != "")).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<VideojuegoModel> ObtenerVideojuego(int SteamAppid)
        {
            try
            {
                return (await _repository.Get(x => x.SteamAppid == SteamAppid)).FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task RegistrarVideojuegoEstiloGenero(VideojuegoModel videojuego, JArray categoriesArray, JArray genresArray)
        {
            try
            {
                //VALIDAR QUE EL JUEGO NO ESTE REGISTRADO YA
                var VideojuegoRegistrado = await ObtenerVideojuego(videojuego.SteamAppid);

                if (VideojuegoRegistrado == null)
                {
                    await _repository.Insert(videojuego);

                    await _unitOfWork.SaveChangesAsync();

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

                    await _unitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<VideojuegoModel> RegistrarObtenerVideojuego(int SteamAppid)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://store.steampowered.com/api/appdetails?appids=" + SteamAppid);
            string json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && json != null)
            {
                // Parse the JSON string
                JObject jsonObject = JObject.Parse(json);

                // Extract values
                string? type = null;
                //type = (string)jsonObject[item.appid.ToString()]["data"]["type"];

                if (jsonObject != null && jsonObject[SteamAppid.ToString()] != null && jsonObject[SteamAppid.ToString()]?["data"] != null && jsonObject[SteamAppid.ToString()]?["data"]?["type"] != null && jsonObject[SteamAppid.ToString()]?["data"]?["categories"] != null && jsonObject[SteamAppid.ToString()]?["data"]?["genres"] != null)
                {
                    type = jsonObject[SteamAppid.ToString()]["data"]["type"].ToString();
                }
                else
                {
                    type = null;
                }

                if (type == "game")
                {
                    VideojuegoModel videojuego = new VideojuegoModel();
                    videojuego.Nombre = jsonObject[SteamAppid.ToString()]["data"]["name"].ToString();
                    videojuego.SteamAppid = SteamAppid;
                    videojuego.Recomendaciones = Convert.ToInt32(jsonObject[SteamAppid.ToString()]?["data"]?["recommendations"]?["total"]);
                    videojuego.Header_image = jsonObject[SteamAppid.ToString()]?["data"]?["header_image"]?.ToString();
                    videojuego.Plataforma_ID = 1;
                    videojuego.Metacritic_score = Convert.ToInt32(jsonObject[SteamAppid.ToString()]?["data"]?["metacritic"]?["score"]);
                    videojuego.Metacritic_url = jsonObject[SteamAppid.ToString()]?["data"]?["metacritic"]?["url"]?.ToString();

                    JArray categoriesArray = (JArray)jsonObject[SteamAppid.ToString()]["data"]["categories"];
                    JArray genresArray = (JArray)jsonObject[SteamAppid.ToString()]["data"]["genres"];

                    var videojuegoModel =  await ObtenerVideojuego(SteamAppid);
                    if (videojuegoModel is null)
                    {
                        await RegistrarVideojuegoEstiloGenero(videojuego, categoriesArray, genresArray);
                    }
                    return videojuegoModel;
                }

                return null;
            }
            else
            {
                throw new Exception("Ocurrio una excepcion al llamar al servicio RegistrarObtenerVideojuego");
            }
        }
    }
}
