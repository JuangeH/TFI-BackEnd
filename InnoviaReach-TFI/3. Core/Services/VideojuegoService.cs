using AutoMapper;
using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Models.Nueva_Base;
using Core.Domain.Request.Gateway;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly IVideojuegoPlataformaRepository _videojuegoPlataformaRepo;
        private readonly IVideojuegoTiendaRepository _videojuegoTiendaRepo;
        private readonly IVideojuegoTagRepository _videojuegoTagRepository;

        private readonly IEstiloRepository _EstiloRepo;
        private readonly IGeneroRepository _GeneroRepo;
        private readonly IPlataformaRepository _PlataformaRepo;
        private readonly IRatingRepository _RatingRepo;
        private readonly ITiendaRepository _TiendaRepo;
        private readonly ITagRepository _TagRepo;
        private readonly IMapper _mapper;

        private VideojuegoPlataformaModel _videojuegoPlataformaModel;
        private VideojuegoGeneroModel _videojuegoGeneroModel;
        private VideojuegoTiendaModel _videojuegoTiendaModel;
        private VideojuegoTagModel _videojuegoTagModel;
        private RatingModel _ratingModel;

        private List<RatingModel> ratingModels;
        private List<GeneroModel> generoModels;
        private List<TiendaModel> tiendaModels;
        private List<TagModel> tagModels;
        private List<PlataformaModel> plataformaModels;

        public VideojuegoService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, unitOfWork.GetRepository<IVideojuegoRepository>())
        {
            _videojuegoEstiloRepo = unitOfWork.GetRepository<IVideojuegoEstiloRepository>();
            _videojuegoGeneroRepo = unitOfWork.GetRepository<IVideojuegoGeneroRepository>();
            _EstiloRepo = unitOfWork.GetRepository<IEstiloRepository>();
            _GeneroRepo = unitOfWork.GetRepository<IGeneroRepository>();
            _mapper = mapper;   
            _PlataformaRepo = unitOfWork.GetRepository<IPlataformaRepository>();
            _RatingRepo = unitOfWork.GetRepository<IRatingRepository>();
            _TiendaRepo = unitOfWork.GetRepository<ITiendaRepository>();
            _TagRepo = unitOfWork.GetRepository<ITagRepository>();
            _videojuegoPlataformaRepo = unitOfWork.GetRepository<IVideojuegoPlataformaRepository>();
            _videojuegoTiendaRepo = unitOfWork.GetRepository<IVideojuegoTiendaRepository>();
            _videojuegoTagRepository = unitOfWork.GetRepository<IVideojuegoTagRepository>();
        }

        public async Task<(List<VideojuegoModel> Videojuegos, int TotalRecords)> ObtenerVideojuegosCatalogo(int pageNumber, int pageSize)
        {
            try
            {
                // Obtener el total de registros directamente de la base de datos
                var totalRecords = await _repository.TableNoTracking.CountAsync(x => x.Nombre != "");

                // Obtener solo los registros para la página actual, aplicando paginación
                var videojuegos = await _repository.TableNoTracking
                    .Where(x => x.Nombre != "")
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (videojuegos, totalRecords);
            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener videojuegos del catálogo");
            }
        }

        public async Task<List<VideojuegoModel>> ObtenerVideojuegos()
        {
            try
            {
                //return (await _repository.Get(x => x.Nombre != "", includeProperties: "Plataforma, videojuegoEstiloModels, videojuegoEstiloModels.estiloModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel")).ToList();
                var videojuegos = (await _repository.Get(x => x.Nombre != "", includeProperties: "ratingModels, videojuegoPlataformaModels, videojuegoPlataformaModels.plataformaModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel, videojuegoTiendaModels, videojuegoTiendaModels.tiendaModel")).ToList();

                foreach (var item in videojuegos)
                {
                    item.videojuegoTagModels = (await _videojuegoTagRepository.Get(x => x.Videojuego_ID == item.Videojuego_ID, includeProperties: "tagModel")).ToList();
                }

                return videojuegos;
            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener videojuegos");
            }
        }

        public async Task<List<VideojuegoModel>> BuscarVideojuegosForo(string nombre, int pageSize)
        {
            try
            {
                var videojuegos = await _repository.TableNoTracking
                   .Where(v => v.Nombre.ToLower().Contains(nombre.ToLower()))
                   .Take(pageSize)
                   .ToListAsync();

                return videojuegos;
            }
            catch (Exception)
            {
                throw new Exception($"Error al buscar videojuegos por foro");
            }
        }


        public async Task<List<VideojuegoModel>> ObtenerVideojuegosForo()
        {
            try
            {
                //return (await _repository.Get(x => x.Nombre != "", includeProperties: "Plataforma, videojuegoEstiloModels, videojuegoEstiloModels.estiloModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel")).ToList();
                var videojuegos = (await _repository.Get(x => x.Nombre != "")).ToList();

                return videojuegos;
            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener videojuegos por foro");
            }
        }

        public async Task<VideojuegoModel> ObtenerVideojuego(int RawgAppId)
        {
            try
            {
                return (await _repository.Get(x => x.AppRawgId == RawgAppId, includeProperties: "videojuegoPlataformaModels, videojuegoPlataformaModels.plataformaModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel, videojuegoTiendaModels, videojuegoTiendaModels.tiendaModel, videojuegoTagModels, videojuegoTagModels.tagModel")).FirstOrDefault();

            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener videojuego");
            }
        }
        public async Task<VideojuegoModel> ObtenerVideojuegoPorNombre(string nombre)
        {
            try
            {
                return (await _repository.Get(x => x.Nombre == nombre, includeProperties: "videojuegoPlataformaModels, videojuegoPlataformaModels.plataformaModel, videojuegoGeneroModels, videojuegoGeneroModels.generoModel, videojuegoTiendaModels, videojuegoTiendaModels.tiendaModel, videojuegoTagModels, videojuegoTagModels.tagModel")).FirstOrDefault();

            }
            catch (Exception)
            {
                throw new Exception($"Error al obtener videojuego {nombre}");
            }
        }

        public async Task AgregarDescripcion()
        {
            try
            {
                var videojuegos = (await _repository.Get(x => x.Nombre != "")).ToList();

                foreach (var item in videojuegos)
                {
                    string apiUrl = "https://api.rawg.io/api/games/" + item.AppRawgId + "?key=6c43806ec84d4f09a9ac4c221d783da2";
                    using (HttpClient httpClient = new HttpClient())
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            VideojuegoRAWG apiResponse = JsonConvert.DeserializeObject<VideojuegoRAWG>(jsonResponse);
                            var traduccion = Regex.Replace(apiResponse.Descripcion, "<.*?>", string.Empty).Trim();
                            item.Descripcion = traduccion;
                            await _repository.Update(item);

                            await _unitOfWork.SaveChangesAsync();

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception($"Error al registrar descripciones");
            }
        }

        public async Task<string> TranslateText(string text, string targetLanguage = "es")
        {
            string url = "https://libretranslate.com/translate";

            // Verifica que los datos cumplan con los requisitos de la API
            var requestBody = new
            {
                q = text, // Texto a traducir
                source = "en", // Idioma original
                target = targetLanguage, // Idioma objetivo
                format = "text" // Formato del texto
            };

            // Crear el cliente HTTP
            using var client = new HttpClient();

            // Serializar el cuerpo de la solicitud a JSON
            var jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            // Hacer la solicitud POST
            var response = await client.PostAsync(url, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Leer y procesar la respuesta
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<LibreTranslateResponse>(jsonResponse);
                return result?.TranslatedText ?? "Error: Traducción no disponible";
            }
            else
            {
                // Manejar errores de la API
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al traducir: {response.StatusCode} - {errorResponse}");
            }
        }

        public class LibreTranslateResponse
        {
            public string TranslatedText { get; set; }
        }



        public async Task RegistrarVideojuegoEstiloGenero(VideojuegoModel videojuego, List<RatingModel> ratings, List<GeneroModel> generos, List<TiendaModel> tiendas, List<TagModel> tags, List<PlataformaModel> plataformas)
        {
            try
            {
                await _repository.Insert(videojuego);

                await _unitOfWork.SaveChangesAsync();

                // Usa ToListAsync para evitar problemas de concurrencia
                var plataformaBase = (await _PlataformaRepo.Get(x => x.Plataforma_ID.ToString() != "")).ToList();
                var generosBase = (await _GeneroRepo.Get(x => x.Genero_ID.ToString() != "")).ToList();
                var tiendaBase = (await _TiendaRepo.Get(x => x.Tienda_ID.ToString() != "")).ToList();
                var tagBase = (await _TagRepo.Get(x => x.Tag_ID.ToString() != "")).ToList();

                foreach (var item in ratings)
                {
                    _ratingModel = new RatingModel();

                    _ratingModel.Videojuego_ID = videojuego.Videojuego_ID;
                    _ratingModel.CantidadVotos = item.CantidadVotos;
                    _ratingModel.Porcentaje = item.Porcentaje;
                    _ratingModel.Titulo = item.Titulo;

                    await _RatingRepo.Insert(_ratingModel);
                }
                await _unitOfWork.SaveChangesAsync();

                foreach (var item in plataformas)
                {
                    // Cargar la lista completa antes del foreach para evitar problemas de concurrencia
                    var plataformaMatch = plataformaBase.FirstOrDefault(x => x.Slug == item.Slug);

                    if (plataformaMatch is null)
                    {
                        await _PlataformaRepo.Insert(plataformaMatch = new PlataformaModel { PlatformRawgID = item.PlatformRawgID, Nombre = item.Nombre, Slug = item.Slug });

                        await _unitOfWork.SaveChangesAsync();
                    }

                    _videojuegoPlataformaModel = new VideojuegoPlataformaModel();

                    _videojuegoPlataformaModel.Videojuego_ID = videojuego.Videojuego_ID;
                    _videojuegoPlataformaModel.Plataforma_ID = plataformaMatch.Plataforma_ID;

                    await _videojuegoPlataformaRepo.Insert(_videojuegoPlataformaModel);
                }

                foreach (var item in generos)
                {
                    var generoMatch = generosBase.FirstOrDefault(x => x.Slug == item.Slug);

                    if (generoMatch is null)
                    {
                        await _GeneroRepo.Insert(generoMatch = new GeneroModel { GenreRawgID = item.GenreRawgID, Nombre = item.Nombre, Slug = item.Slug });

                        await _unitOfWork.SaveChangesAsync();
                    }

                    _videojuegoGeneroModel = new VideojuegoGeneroModel();

                    _videojuegoGeneroModel.Videojuego_ID = videojuego.Videojuego_ID;
                    _videojuegoGeneroModel.Genero_ID = generoMatch.Genero_ID;

                    await _videojuegoGeneroRepo.Insert(_videojuegoGeneroModel);
                }

                foreach (var item in tiendas)
                {
                    var tiendaMatch = tiendaBase.FirstOrDefault(x => x.Slug == item.Slug);

                    if (tiendaMatch is null)
                    {
                        await _TiendaRepo.Insert(tiendaMatch = new TiendaModel { StoreRawgId = item.StoreRawgId, Nombre = item.Nombre, Slug = item.Slug, Dominio = item.Dominio });

                        await _unitOfWork.SaveChangesAsync();
                    }

                    _videojuegoTiendaModel = new VideojuegoTiendaModel();

                    _videojuegoTiendaModel.Videojuego_ID = videojuego.Videojuego_ID;
                    _videojuegoTiendaModel.Tienda_ID = tiendaMatch.Tienda_ID;

                    await _videojuegoTiendaRepo.Insert(_videojuegoTiendaModel);
                }

                foreach (var item in tags)
                {
                    var tagMatch = tagBase.FirstOrDefault(x => x.Slug == item.Slug);

                    if (tagMatch is null)
                    {
                        await _TagRepo.Insert(tagMatch = new TagModel { TagRawgId = item.TagRawgId, Nombre = item.Nombre, Slug = item.Slug });

                        await _unitOfWork.SaveChangesAsync();
                    }

                    _videojuegoTagModel = new VideojuegoTagModel();

                    _videojuegoTagModel.Videojuego_ID = videojuego.Videojuego_ID;
                    _videojuegoTagModel.Tag_ID = tagMatch.Tag_ID;

                    await _videojuegoTagRepository.Insert(_videojuegoTagModel);
                }

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al registrar información del videojuego {videojuego.Nombre}");
            }
        }


        public async Task<VideojuegoModel> RegistrarObtenerVideojuego(VideojuegoRAWG videojuego)
        {
            try
            {
                var videojuegoModel = await ObtenerVideojuego(videojuego.Id);
                if (videojuegoModel is null)
                {
                    VideojuegoModel videojuegoMod = new VideojuegoModel();

                    videojuegoMod.AppRawgId = videojuego.Id;
                    videojuegoMod.Slug = videojuego.Slug;
                    videojuegoMod.Nombre = videojuego.Name;
                    videojuegoMod.FechaSalida = videojuego.Released;
                    videojuegoMod.Imagen = videojuego.BackgroundImage;
                    videojuegoMod.Rating = videojuego.Rating;
                    videojuegoMod.Metacritic = videojuego.Metacritic;
                    videojuegoMod.Descripcion = videojuego.Descripcion;

                    ratingModels = new List<RatingModel>();
                    generoModels = new List<GeneroModel>();
                    tiendaModels = new List<TiendaModel>();
                    tagModels = new List<TagModel>();
                    plataformaModels = new List<PlataformaModel>();

                    ratingModels = _mapper.Map<List<RatingModel>>(videojuego.Ratings);
                    generoModels = _mapper.Map<List<GeneroModel>>(videojuego.Genres);
                    tagModels = _mapper.Map<List<TagModel>>(videojuego.Tags);

                    foreach (var item in videojuego.Stores)
                    {
                        tiendaModels.Add(_mapper.Map<TiendaModel>(item.StoreInfo));
                    }

                    foreach (var item in videojuego.ParentPlatforms)
                    {
                        plataformaModels.Add(_mapper.Map<PlataformaModel>(item.Platform));
                    }

                    await RegistrarVideojuegoEstiloGenero(videojuegoMod, ratingModels, generoModels, tiendaModels, tagModels, plataformaModels);
                }

                videojuegoModel = await ObtenerVideojuego(videojuego.Id);

                return videojuegoModel;
            }
            catch (Exception)
            {
                throw new Exception($"Error al registrar el videojuego {videojuego.Name}");
            }
        }
    }
}
