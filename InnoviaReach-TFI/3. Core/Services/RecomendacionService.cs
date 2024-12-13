using API_Business.Response;
using AutoMapper;
using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Transversal.Extensions;

namespace _3._Core.Services
{
    public class RecomendacionService : GenericService<RecomendacionModel>, IRecomendacionService
    {
        private MLContext mlContext = new MLContext();

        private IForoRepository _foroRepository;
        private IForoUsuarioVisitaRepository _foroUsuarioVisitaRepository;
        private IVideojuegoRepository _videojuegoRepository;
        private IUsuarioJuegoPerfilRepository _usuarioJuegoPerfilRepository;
        private IUsersRepository _usersRepository;
        private IRecomendacionVideojuegoRepository _recomendacionVideojuegoRepository;
        private IRecomendacionUsuarioRepository _recomendacionUsuarioRepository;
        private ITagRepository _tagRepository;
        private IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ComentarioService> _logger;

        public RecomendacionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ComentarioService> logger)
            : base(unitOfWork, unitOfWork.GetRepository<IRecomendacionRepository>())
        {
            _foroRepository = unitOfWork.GetRepository<IForoRepository>();
            _foroUsuarioVisitaRepository = unitOfWork.GetRepository<IForoUsuarioVisitaRepository>();
            _videojuegoRepository = unitOfWork.GetRepository<IVideojuegoRepository>();
            _usersRepository = unitOfWork.GetRepository<IUsersRepository>();
            _recomendacionVideojuegoRepository = unitOfWork.GetRepository<IRecomendacionVideojuegoRepository>();
            _mapper = mapper;
            _tagRepository = unitOfWork.GetRepository<ITagRepository>();
            _generoRepository = unitOfWork.GetRepository<IGeneroRepository>();
            _usuarioJuegoPerfilRepository = unitOfWork.GetRepository<IUsuarioJuegoPerfilRepository>();
            _recomendacionUsuarioRepository = unitOfWork.GetRepository<IRecomendacionUsuarioRepository>();
            _logger = logger;
        }

        #region VIEJO

        

        // Diccionario donde se almacena el Estilo_ID, Nombre_Estilo y la cantidad de veces que aparece
        Dictionary<int, Tuple<string, int>> keyValuePairsEstilos = new Dictionary<int, Tuple<string, int>>();
        Dictionary<int, Tuple<string, int>> keyValuePairsGeneros = new Dictionary<int, Tuple<string, int>>();

        public async Task<List<ForoModel>> RecomendacionesPorVisitas(string User_ID)
        {
            try
            {
                var foroUsuarioVisitaList = (await _foroUsuarioVisitaRepository.Get(x => x.User_ID == User_ID, includeProperties: "foro,foro.videojuego,foro.videojuego.videojuegoEstiloModels,foro.videojuego.videojuegoGeneroModels,foro.videojuego.videojuegoEstiloModels.estiloModel,foro.videojuego.videojuegoGeneroModels.generoModel")).ToList();
                //var foroUsuarioVisitaListGeneros = (await _foroUsuarioVisitaRepository.Get(x => x.User_ID == User_ID, includeProperties: "foro,foro.videojuego,foro.videojuego.videojuegoEstiloModels,foro.videojuego.videojuegoGeneroModels,foro.videojuego.videojuegoGeneroModels.generoModel")).ToList();

                foreach (var item in foroUsuarioVisitaList)
                {
                    GestionarTopEstilo(item.foro.videojuego.videojuegoEstiloModels.ToList());
                    GestionarTopGenero(item.foro.videojuego.videojuegoGeneroModels.ToList());
                }
                //foreach (var item in foroUsuarioVisitaListGeneros)
                //{
                //    GestionarTopGenero(item.foro.videojuego.videojuegoGeneroModels.ToList());
                //}

                var topEstilo = keyValuePairsEstilos.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();
                var topGenero = keyValuePairsGeneros.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();

                var top10Foros = (await _foroRepository.Get(x => x.videojuego.videojuegoEstiloModels.Any(y => y.Estilo_ID == topEstilo.Key) &&
                    x.videojuego.videojuegoGeneroModels.Any(y => y.Genero_ID == topGenero.Key),
                    includeProperties: "videojuego,videojuego.videojuegoEstiloModels,videojuego.videojuegoGeneroModels"))
                    .Take(10).ToList();

                return top10Foros;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GestionarTopEstilo(List<VideojuegoEstiloModel> estilos)
        {
            foreach (var item2 in estilos)
            {
                // Verificar si el Estilo_ID ya está en el diccionario
                if (keyValuePairsEstilos.ContainsKey(item2.Estilo_ID))
                {
                    // Si está, actualizamos la cantidad
                    var currentTuple = keyValuePairsEstilos[item2.Estilo_ID];
                    keyValuePairsEstilos[item2.Estilo_ID] = new Tuple<string, int>(currentTuple.Item1, currentTuple.Item2 + 1);
                }
                else
                {
                    // Si no está, lo añadimos con cantidad 1
                    keyValuePairsEstilos[item2.Estilo_ID] = new Tuple<string, int>(item2.estiloModel.Descripcion, 1);
                }
            }

            //// Aquí puedes hacer algo con el diccionario, como obtener el estilo más repetido
            //// Esto es solo un ejemplo de cómo podrías obtener el más repetido:
            //var topEstilo = keyValuePairsEstilos.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();

            //// Retornar el ID del estilo más repetido o hacer lo que necesites
            //return topEstilo.Key;
        }
        private void GestionarTopGenero(List<VideojuegoGeneroModel> generos)
        {
            foreach (var item2 in generos)
            {
                // Verificar si el Estilo_ID ya está en el diccionario
                if (keyValuePairsGeneros.ContainsKey(item2.Genero_ID))
                {
                    // Si está, actualizamos la cantidad
                    var currentTuple = keyValuePairsGeneros[item2.Genero_ID];
                    keyValuePairsGeneros[item2.Genero_ID] = new Tuple<string, int>(currentTuple.Item1, currentTuple.Item2 + 1);
                }
                else
                {
                    // Si no está, lo añadimos con cantidad 1
                    keyValuePairsGeneros[item2.Genero_ID] = new Tuple<string, int>(item2.generoModel.Nombre, 1);
                }
            }

            //// Aquí puedes hacer algo con el diccionario, como obtener el estilo más repetido
            //// Esto es solo un ejemplo de cómo podrías obtener el más repetido:
            //var topEstilo = keyValuePairsEstilos.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();

            //// Retornar el ID del estilo más repetido o hacer lo que necesites
            //return topEstilo.Key;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------

        #region RECOMENDACIONES-ELEMENTOS COMUNES
        // Clases auxiliares para almacenar los resultados del clustering
        public class PredictedCluster
        {
            public uint PredictedLabel { get; set; }
            [VectorType] public float[] CaracteristicasVector { get; set; }
            public float[] ReducedFeatures { get; set; }
        }

        public class PredictedUserCluster
        {
            public uint PredictedLabel { get; set; }
            [VectorType] public float[] Features { get; set; }
        }

        #endregion

        #region RECOMENDACIONES VIDEOJUEGOS

        public async Task CrearClusters(List<VideojuegoClusterModel> juegos)
        {
            try
            {
                // 1. Preparar los datos
                var data = mlContext.Data.LoadFromEnumerable(juegos);

                // 2. Transformar texto a vector de características y aplicar reducción de dimensionalidad (PCA)
                int sqrtCount = (int)Math.Sqrt(juegos.Count);
                int pcaDimensions = Math.Min(50, sqrtCount * 2);

                var pipeline = mlContext.Transforms.Text.FeaturizeText("CaracteristicasVector", "Caracteristicas")
                    .Append(mlContext.Transforms.ProjectToPrincipalComponents("ReducedFeatures", "CaracteristicasVector", rank: pcaDimensions))
                    .Append(mlContext.Clustering.Trainers.KMeans("ReducedFeatures", numberOfClusters: sqrtCount));

                // 3. Entrenar el modelo
                var model = pipeline.Fit(data);
                var predictions = model.Transform(data);

                // 4. Asignar ClusterID y CaracteristicasVector reducido a cada juego
                var clusters = mlContext.Data.CreateEnumerable<PredictedCluster>(predictions, reuseRowObject: false).ToList();
                for (int i = 0; i < juegos.Count; i++)
                {
                    juegos[i].ClusterID = Convert.ToInt32(clusters[i].PredictedLabel);
                    juegos[i].CaracteristicasVector = clusters[i].ReducedFeatures;
                }

                //5.Actualizar la base de datos
                foreach (var item in juegos)
                {
                    var videojuego = (await _videojuegoRepository.Get(x => x.AppRawgId == item.AppRawgId)).FirstOrDefault();

                    videojuego.ClusterID = item.ClusterID;
                    videojuego.CaracteristicasVector = JsonConvert.SerializeObject(item.CaracteristicasVector);

                    await _videojuegoRepository.Update(videojuego);
                }

                _logger.LogBusiness($"Se crearon los clusters para videojuegos exitosamente.");

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception($"Error al crear clusters de videojuegos");
            }
        }

        // Método para calcular similitud de coseno
        public float CalcularSimilitudCoseno(float[] vectorA, float[] vectorB)
        {
            try
            {
                float productoPunto = 0f;
                float magnitudA = 0f;
                float magnitudB = 0f;

                for (int i = 0; i < vectorA.Length; i++)
                {
                    productoPunto += vectorA[i] * vectorB[i];
                    magnitudA += vectorA[i] * vectorA[i];
                    magnitudB += vectorB[i] * vectorB[i];
                }

                return productoPunto / (float)(Math.Sqrt(magnitudA) * Math.Sqrt(magnitudB));
            }
            catch (Exception)
            {
                throw new Exception("Error al calcular similitud de coseno");
            }
        }

        // Método para generar recomendaciones
        public async Task GenerarRecomendaciones(VideojuegoModel juegoReferencia, string Usuario_ID, string TipoRecomendacion)
        {
            try
            {
                var CaractVectorJuegoRef = JsonConvert.DeserializeObject<float[]>(juegoReferencia.CaracteristicasVector);

                // 1. Filtrar juegos en el mismo cluster que el juego de referencia
                var juegosCluster = _mapper.Map<List<VideojuegoRecModel>>(await _videojuegoRepository.Get(x => x.ClusterID == juegoReferencia.ClusterID)).ToList();

                // 2. Calcular similitud de coseno y asignarla al objeto `VideojuegoClusterModel`
                foreach (var juego in juegosCluster.Where(j => j.Videojuego_ID != juegoReferencia.Videojuego_ID))
                {
                    // Deserializar los vectores de características
                    var vectorB = JsonConvert.DeserializeObject<float[]>(juego.CaracteristicasVector);

                    // Calcular similitud de coseno
                    juego.Similitud = CalcularSimilitudCoseno(CaractVectorJuegoRef, vectorB);
                }

                // 3. Ordenar los juegos por similitud y seleccionar los 10 más similares
                var recomendaciones = juegosCluster
                    .Where(j => j.Videojuego_ID != juegoReferencia.Videojuego_ID)
                    .OrderByDescending(j => j.Similitud)
                    .Take(10)
                .ToList();

                var recomendacion = (await _recomendacionVideojuegoRepository.Get(x => x.VideojuegoReferenciaId == juegoReferencia.Videojuego_ID && x.UserId == Usuario_ID && x.TipoRecomendacion == TipoRecomendacion)).ToList();

                if (recomendacion != null)
                {
                    foreach (var item in recomendacion)
                    {
                        await _recomendacionVideojuegoRepository.Delete(item);
                    }
                    await _unitOfWork.SaveChangesAsync();
                }

                foreach (var item in recomendaciones)
                {
                    RecomendacionVideojuegoModel recomendacionVideojuegoModel = new RecomendacionVideojuegoModel();

                    recomendacionVideojuegoModel.FechaRecomendacion = DateTime.Now;
                    recomendacionVideojuegoModel.VideojuegoReferenciaId = juegoReferencia.Videojuego_ID;
                    recomendacionVideojuegoModel.VideojuegoRecomendadoId = item.Videojuego_ID;
                    recomendacionVideojuegoModel.UserId = Usuario_ID;
                    recomendacionVideojuegoModel.Similitud = item.Similitud;
                    recomendacionVideojuegoModel.TipoRecomendacion = TipoRecomendacion;

                    //var vidjuego = await _recomendacionVideojuegoRepository.Get(x => x.VideojuegoReferenciaId == juegoReferencia.Videojuego_ID);

                    //if (vidjuego is null)
                    //{
                    //await _recomendacionVideojuegoRepository.Insert(recomendacionVideojuegoModel);
                    //}
                    //else
                    //{
                    //await _recomendacionVideojuegoRepository.Delete(vidjuego);
                    await _recomendacionVideojuegoRepository.Insert(recomendacionVideojuegoModel);
                    //}

                    await _unitOfWork.SaveChangesAsync();
                }

                _logger.LogBusiness($"Se generaron las recomendaciones para videojuegos exitosamente.");


            }
            catch (Exception)
            {
                throw new Exception("Error generar recomendaciones de videojuegos}");
            }
        }

        #endregion

        #region RECOMENDACIONES ACTIVIDAD-USUARIO
        public async Task CrearClustersUsuarios()
        {
            try
            {
                //VISITA
                var usersVisita = (await _usersRepository.Get(
                    includeProperties: "usuarioVisitaModels, usuarioVisitaModels.Videojuego, usuarioVisitaModels.Videojuego.videojuegoGeneroModels.generoModel, usuarioVisitaModels.Videojuego.videojuegoTagModels.tagModel",
                    tracking: false)).ToList();

                //FOROFAV
                var usersForo = (await _usersRepository.Get(includeProperties: "foroUsuarioFavoritoModels, foroUsuarioFavoritoModels.foro, foroUsuarioFavoritoModels.foro.videojuego, foroUsuarioFavoritoModels.foro.videojuego.videojuegoGeneroModels.generoModel, foroUsuarioFavoritoModels.foro.videojuego.videojuegoTagModels.tagModel",
                    tracking: false)).ToList();

                //VISITA
                var perfilesUsuariosVisita = ConvertirAUserProfiles(usersVisita, "Visita");
                int sqrtCountVisita = (int)Math.Sqrt(perfilesUsuariosVisita.Count);

                //FOROFAV
                var perfilesUsuariosForoFav = ConvertirAUserProfiles(usersForo, "ForoFav");
                int sqrtCountForoFav = (int)Math.Sqrt(perfilesUsuariosForoFav.Count);

                //GENERAL
                var generos = await _generoRepository.Get();
                var tags = await _tagRepository.Get();

                // Definir el vocabulario de géneros y tags para visita
                List<string> vocabularioGenerosVisita = new List<string>();
                List<string> vocabularioTagsVisita = new List<string>();
                vocabularioGenerosVisita = generos.Select(g => g.Nombre).Distinct().ToList();
                vocabularioTagsVisita = tags.Select(g => g.Nombre).Distinct().ToList();

                // Definir el vocabulario de géneros y tags para ForoFav
                List<string> vocabularioGenerosForoFav = new List<string>();
                List<string> vocabularioTagsForoFav = new List<string>();
                vocabularioGenerosForoFav = generos.Select(g => g.Nombre).Distinct().ToList();
                vocabularioTagsForoFav = tags.Select(g => g.Nombre).Distinct().ToList();

                //VISITA
                int longitudGenerosVisita = vocabularioGenerosVisita.Count;
                int longitudTagsVisita = vocabularioTagsVisita.Count;
                int longitudTotalVisita = longitudGenerosVisita + longitudTagsVisita;

                //FOROFAV
                int longitudGenerosForoFav = vocabularioGenerosForoFav.Count;
                int longitudTagsForoFav = vocabularioTagsForoFav.Count;
                int longitudTotalForoFav = longitudGenerosForoFav + longitudTagsForoFav;

                //VISITA
                var schemaDefinitionVisita = SchemaDefinition.Create(typeof(UsuarioClusterDataModel));
                schemaDefinitionVisita["GenresVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudGenerosVisita);
                schemaDefinitionVisita["TagsVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudTagsVisita);

                // Transformar los perfiles
                TransformarPerfiles(perfilesUsuariosVisita, vocabularioGenerosVisita, vocabularioTagsVisita);

                // Preparar los datos para ML.NET
                var datosMLVisita = PrepararDatosParaML(perfilesUsuariosVisita, longitudGenerosVisita, longitudTagsVisita);
                var dataVisita = mlContext.Data.LoadFromEnumerable(datosMLVisita, schemaDefinitionVisita);

                var pipelineVisita = mlContext.Transforms
                    .Concatenate("Features", "GenresVector", "TagsVector")
                    .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: sqrtCountVisita));

                var modelVisita = pipelineVisita.Fit(dataVisita);
                var predictionsVisita = modelVisita.Transform(dataVisita);

                // Asignar el ClusterID a cada perfil de usuario
                var clustersVisita = mlContext.Data.CreateEnumerable<PredictedUserCluster>(predictionsVisita, reuseRowObject: false).ToList();
                for (int i = 0; i < perfilesUsuariosVisita.Count; i++)
                {
                    perfilesUsuariosVisita[i].ClusterID = (int)clustersVisita[i].PredictedLabel;
                }

                foreach (var perfil in perfilesUsuariosVisita)
                {
                    var usuarioJuegoPerfil = (await _usuarioJuegoPerfilRepository
                        .Get(p => p.User_ID == perfil.UserId && p.TipoRecomendacion == "Visita")).FirstOrDefault();

                    if (usuarioJuegoPerfil == null)
                    {
                        // Si no existe el perfil, crearlo
                        usuarioJuegoPerfil = new UsuarioJuegoPerfilModel
                        {
                            User_ID = perfil.UserId,
                            ClusterID = perfil.ClusterID,
                            TipoRecomendacion = "Visita",
                            GameGenresJson = JsonConvert.SerializeObject(perfil.GameGenres),
                            GameTagsJson = JsonConvert.SerializeObject(perfil.GameTags),
                            GameHistoryJson = JsonConvert.SerializeObject(perfil.GameHistory)
                        };

                        await _usuarioJuegoPerfilRepository.Insert(usuarioJuegoPerfil);
                    }
                    else
                    {
                        // Si existe, actualizar los campos
                        usuarioJuegoPerfil.User_ID = perfil.UserId;
                        usuarioJuegoPerfil.ClusterID = perfil.ClusterID;
                        usuarioJuegoPerfil.TipoRecomendacion = "Visita";
                        usuarioJuegoPerfil.GameGenresJson = JsonConvert.SerializeObject(perfil.GameGenres);
                        usuarioJuegoPerfil.GameTagsJson = JsonConvert.SerializeObject(perfil.GameTags);
                        usuarioJuegoPerfil.GameHistoryJson = JsonConvert.SerializeObject(perfil.GameHistory);

                        await _usuarioJuegoPerfilRepository.Update(usuarioJuegoPerfil);
                    }
                }

                await _unitOfWork.SaveChangesAsync();

                //FOROFAV
                var schemaDefinitionForoFav = SchemaDefinition.Create(typeof(UsuarioClusterDataModel));
                schemaDefinitionForoFav["GenresVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudGenerosForoFav);
                schemaDefinitionForoFav["TagsVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudTagsForoFav);

                // Transformar los perfiles
                TransformarPerfiles(perfilesUsuariosForoFav, vocabularioGenerosForoFav, vocabularioTagsForoFav);

                // Preparar los datos para ML.NET
                var datosMLForoFav = PrepararDatosParaML(perfilesUsuariosForoFav, longitudGenerosForoFav, longitudTagsForoFav);
                var dataForoFav = mlContext.Data.LoadFromEnumerable(datosMLForoFav, schemaDefinitionForoFav);

                var pipelineForoFav = mlContext.Transforms
                    .Concatenate("Features", "GenresVector", "TagsVector")
                    .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: sqrtCountForoFav));

                var modelForoFav = pipelineForoFav.Fit(dataForoFav);
                var predictionsForoFav = modelForoFav.Transform(dataForoFav);

                // Asignar el ClusterID a cada perfil de usuario
                var clustersForoFav = mlContext.Data.CreateEnumerable<PredictedUserCluster>(predictionsForoFav, reuseRowObject: false).ToList();
                for (int i = 0; i < perfilesUsuariosForoFav.Count; i++)
                {
                    perfilesUsuariosForoFav[i].ClusterID = (int)clustersForoFav[i].PredictedLabel;
                }

                foreach (var perfil in perfilesUsuariosForoFav)
                {
                    var usuarioJuegoPerfil = (await _usuarioJuegoPerfilRepository
                        .Get(p => p.User_ID == perfil.UserId && p.TipoRecomendacion == "ForoFav")).FirstOrDefault();

                    if (usuarioJuegoPerfil == null)
                    {
                        // Si no existe el perfil, crearlo
                        usuarioJuegoPerfil = new UsuarioJuegoPerfilModel
                        {
                            User_ID = perfil.UserId,
                            ClusterID = perfil.ClusterID,
                            TipoRecomendacion = "ForoFav",
                            GameGenresJson = JsonConvert.SerializeObject(perfil.GameGenres),
                            GameTagsJson = JsonConvert.SerializeObject(perfil.GameTags),
                            GameHistoryJson = JsonConvert.SerializeObject(perfil.GameHistory)
                        };

                        await _usuarioJuegoPerfilRepository.Insert(usuarioJuegoPerfil);
                    }
                    else
                    {
                        // Si existe, actualizar los campos
                        usuarioJuegoPerfil.User_ID = perfil.UserId;
                        usuarioJuegoPerfil.ClusterID = perfil.ClusterID;
                        usuarioJuegoPerfil.TipoRecomendacion = "ForoFav";
                        usuarioJuegoPerfil.GameGenresJson = JsonConvert.SerializeObject(perfil.GameGenres);
                        usuarioJuegoPerfil.GameTagsJson = JsonConvert.SerializeObject(perfil.GameTags);
                        usuarioJuegoPerfil.GameHistoryJson = JsonConvert.SerializeObject(perfil.GameHistory);

                        await _usuarioJuegoPerfilRepository.Update(usuarioJuegoPerfil);
                    }
                }

                _logger.LogBusiness($"Se crearon los clusters de usuarios exitosamente.");

                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error al crear clusters de usuarios}");
            }
        }
        private float CalcularSimilitudCosenoUsuario(Dictionary<string, int> vectorA, Dictionary<string, int> vectorB)
        {
            try
            {
                var keys = vectorA.Keys.Intersect(vectorB.Keys);
                float productoPunto = keys.Sum(k => vectorA[k] * vectorB[k]);

                float magnitudA = (float)Math.Sqrt(vectorA.Values.Sum(x => x * x));
                float magnitudB = (float)Math.Sqrt(vectorB.Values.Sum(x => x * x));

                // Manejar el caso en que una de las magnitudes sea cero
                if (magnitudA == 0 || magnitudB == 0)
                    return 0;

                return productoPunto / (magnitudA * magnitudB);
            }
            catch (Exception)
            {
                throw new Exception("Error al calcular similitud de coseno");
            }
        }
        private async Task GenerarRecomendacionesColaborativas(string userId)
        {
            try
            {
                var perfilesJuego = (await _usuarioJuegoPerfilRepository.Get(p => p.User_ID == userId)).ToList();

                if (perfilesJuego == null || !perfilesJuego.Any())
                {
                    throw new Exception($"Perfil de usuario con ID {userId} no encontrado.");
                }

                foreach (var perfilJuego in perfilesJuego)
                {
                    var perfilUsuario = new UsuarioClusterModel
                    {
                        UserId = perfilJuego.User_ID,
                        ClusterID = perfilJuego.ClusterID ?? -1,
                        GameGenres = string.IsNullOrEmpty(perfilJuego.GameGenresJson)
                            ? new Dictionary<string, int>()
                            : JsonConvert.DeserializeObject<Dictionary<string, int>>(perfilJuego.GameGenresJson),
                        GameTags = string.IsNullOrEmpty(perfilJuego.GameTagsJson)
                            ? new Dictionary<string, int>()
                            : JsonConvert.DeserializeObject<Dictionary<string, int>>(perfilJuego.GameTagsJson),
                        GameHistory = string.IsNullOrEmpty(perfilJuego.GameHistoryJson)
                            ? new List<string>()
                            : JsonConvert.DeserializeObject<List<string>>(perfilJuego.GameHistoryJson)
                    };

                    // Obtener todos los usuarios del mismo cluster
                    var users = await _usuarioJuegoPerfilRepository.Get(filter: u => u.ClusterID == perfilUsuario.ClusterID && u.User_ID != userId && u.TipoRecomendacion == perfilJuego.TipoRecomendacion);

                    // Crear perfiles de usuario a partir del JSON
                    var userProfiles = users.Select(u => new UsuarioClusterModel
                    {
                        UserId = u.User_ID,
                        ClusterID = u.ClusterID ?? -1,
                        GameGenres = string.IsNullOrEmpty(u.GameGenresJson)
                            ? new Dictionary<string, int>()
                            : JsonConvert.DeserializeObject<Dictionary<string, int>>(u.GameGenresJson),
                        GameTags = string.IsNullOrEmpty(u.GameTagsJson)
                            ? new Dictionary<string, int>()
                            : JsonConvert.DeserializeObject<Dictionary<string, int>>(u.GameTagsJson),
                        GameHistory = string.IsNullOrEmpty(u.GameHistoryJson)
                            ? new List<string>()
                            : JsonConvert.DeserializeObject<List<string>>(u.GameHistoryJson)
                    }).ToList();

                    // Filtrar usuarios y calcular similitud de coseno
                    var usuariosSimilares = userProfiles
                        .Select(u => new
                        {
                            Usuario = u,
                            Similitud = CalcularSimilitudCosenoUsuario(perfilUsuario.GameGenres, u.GameGenres) +
                                        CalcularSimilitudCosenoUsuario(perfilUsuario.GameTags, u.GameTags)
                        })
                        .OrderByDescending(x => x.Similitud)
                        .ToList();

                    // Crear un diccionario de juegos recomendados
                    var recomendaciones = new Dictionary<string, int>();

                    foreach (var usuarioSimilar in usuariosSimilares.Take(5))
                    {
                        foreach (var juego in usuarioSimilar.Usuario.GameHistory)
                        {
                            if (!perfilUsuario.GameHistory.Contains(juego)) // Solo recomendar juegos no vistos
                            {
                                if (recomendaciones.ContainsKey(juego))
                                    recomendaciones[juego]++;
                                else
                                    recomendaciones[juego] = 1;
                            }
                        }
                    }

                    // Ordenar juegos recomendados por popularidad
                    var resultado = recomendaciones.OrderByDescending(r => r.Value).ToList();

                    // Registrar las recomendaciones
                    foreach (var recomendacion in resultado)
                    {
                        // Busca el videojuego por nombre (puedes ajustar esto según tu estructura de datos)
                        var videojuego = await _videojuegoRepository.GetOne(v => v.Nombre == recomendacion.Key);

                        if (videojuego != null)
                        {
                            var rec = (await _recomendacionUsuarioRepository.Get(x => x.UserId == userId && x.VideojuegoRecomendadoId == videojuego.Videojuego_ID)).FirstOrDefault();

                            // Crear la recomendación
                            var nuevaRecomendacion = new RecomendacionUsuarioModel
                            {
                                UserId = userId,
                                VideojuegoRecomendadoId = videojuego.Videojuego_ID,
                                Frecuencia = recomendacion.Value,
                                TipoRecomendacion = perfilJuego.TipoRecomendacion,
                                FechaRecomendacion = DateTime.UtcNow
                            };

                            if (rec == null)
                            {
                                await _recomendacionUsuarioRepository.Insert(nuevaRecomendacion);
                            }
                            else
                            {
                                await _recomendacionUsuarioRepository.Update(nuevaRecomendacion);
                            }
                        }
                    }
                }
                // Guardar los cambios
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error al generar recomendaciones colaborativas");
            }
        }
        public async Task GenerarRecomendacionesColaborativas()
        {
            try
            {
                var userRepository = _unitOfWork.GetRepository<IUsersRepository>();
                foreach (var user in await userRepository.Get())
                {
                    await GenerarRecomendacionesColaborativas(user.Id);
                }
                _logger.LogBusiness($"Se generaron las creaciones colaborativas para usuarios exitosamente.");
            }
            catch (Exception)
            {
                throw new Exception("Error al generar recomendaciones colaborativas");
            }

            
        }
        public void TransformarPerfiles(List<UsuarioClusterModel> perfilesUsuarios, List<string> vocabularioGeneros, List<string> vocabularioTags)
        {
            try
            {
                int longitudGeneros = vocabularioGeneros.Count;
                int longitudTags = vocabularioTags.Count;

                foreach (var perfil in perfilesUsuarios)
                {
                    perfil.GenresVector = new float[longitudGeneros];
                    perfil.TagsVector = new float[longitudTags];

                    for (int i = 0; i < longitudGeneros; i++)
                    {
                        string genero = vocabularioGeneros[i];
                        perfil.GenresVector[i] = perfil.GameGenres.ContainsKey(genero) ? (float)perfil.GameGenres[genero] : 0f;
                    }

                    for (int i = 0; i < longitudTags; i++)
                    {
                        string tag = vocabularioTags[i];
                        perfil.TagsVector[i] = perfil.GameTags.ContainsKey(tag) ? (float)perfil.GameTags[tag] : 0f;
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al transformar perfiles");
            }
        }
        public List<UsuarioClusterDataModel> PrepararDatosParaML(List<UsuarioClusterModel> perfilesUsuarios, int longitudGeneros, int longitudTags)
        {
            try
            {
                var datosML = new List<UsuarioClusterDataModel>();

                foreach (var perfil in perfilesUsuarios)
                {
                    var genresVector = new float[longitudGeneros];
                    var tagsVector = new float[longitudTags];

                    // Asignar valores al vector de géneros
                    for (int i = 0; i < longitudGeneros; i++)
                    {
                        genresVector[i] = i < perfil.GenresVector.Length ? perfil.GenresVector[i] : 0f;
                    }

                    // Asignar valores al vector de tags
                    for (int i = 0; i < longitudTags; i++)
                    {
                        tagsVector[i] = i < perfil.TagsVector.Length ? perfil.TagsVector[i] : 0f;
                    }

                    datosML.Add(new UsuarioClusterDataModel
                    {
                        UserId = perfil.UserId,
                        GenresVector = genresVector,
                        TagsVector = tagsVector
                    });
                }

                return datosML;
            }
            catch (Exception)
            {
                throw new Exception("Error al preparar datos para Machine Learning");
            }
        }
        public List<UsuarioClusterModel> ConvertirAUserProfiles(List<Users> users, string Tipo)
        {
            try
            {
                var userProfiles = new List<UsuarioClusterModel>();

                foreach (var user in users)
                {
                    // Inicializar diccionarios para géneros y tags
                    var gameGenres = new Dictionary<string, int>();
                    var gameTags = new Dictionary<string, int>();
                    var gameHistory = new List<string>();

                    if (Tipo == "Visita")
                    {
                        // Iterar sobre las visitas del usuario
                        foreach (var visita in user.usuarioVisitaModels)
                        {
                            var videojuego = visita.Videojuego;

                            // Agregar el videojuego al historial
                            if (!gameHistory.Contains(videojuego.Nombre))
                            {
                                gameHistory.Add(videojuego.Nombre);
                            }

                            // Contabilizar géneros
                            foreach (var generoModel in videojuego.videojuegoGeneroModels)
                            {
                                var genero = generoModel.generoModel.Nombre;
                                if (gameGenres.ContainsKey(genero))
                                {
                                    gameGenres[genero]++;
                                }
                                else
                                {
                                    gameGenres[genero] = 1;
                                }
                            }

                            // Contabilizar tags
                            foreach (var tagModel in videojuego.videojuegoTagModels)
                            {
                                var tag = tagModel.tagModel.Nombre;
                                if (gameTags.ContainsKey(tag))
                                {
                                    gameTags[tag]++;
                                }
                                else
                                {
                                    gameTags[tag] = 1;
                                }
                            }
                        }

                        // Crear el UserProfile y añadirlo a la lista
                        var userProfile = new UsuarioClusterModel
                        {
                            UserId = user.Id,
                            GameGenres = gameGenres,
                            GameTags = gameTags,
                            GameHistory = gameHistory
                        };

                        userProfiles.Add(userProfile);
                    }
                    else
                    {
                        // Iterar sobre las visitas del usuario
                        foreach (var visita in user.foroUsuarioFavoritoModels)
                        {
                            var videojuego = visita.foro.videojuego;

                            // Agregar el videojuego al historial
                            if (!gameHistory.Contains(videojuego.Nombre))
                            {
                                gameHistory.Add(videojuego.Nombre);
                            }

                            // Contabilizar géneros
                            foreach (var generoModel in videojuego.videojuegoGeneroModels)
                            {
                                var genero = generoModel.generoModel.Nombre;
                                if (gameGenres.ContainsKey(genero))
                                {
                                    gameGenres[genero]++;
                                }
                                else
                                {
                                    gameGenres[genero] = 1;
                                }
                            }

                            // Contabilizar tags
                            foreach (var tagModel in videojuego.videojuegoTagModels)
                            {
                                var tag = tagModel.tagModel.Nombre;
                                if (gameTags.ContainsKey(tag))
                                {
                                    gameTags[tag]++;
                                }
                                else
                                {
                                    gameTags[tag] = 1;
                                }
                            }
                        }

                        // Crear el UserProfile y añadirlo a la lista
                        var userProfile = new UsuarioClusterModel
                        {
                            UserId = user.Id,
                            GameGenres = gameGenres,
                            GameTags = gameTags,
                            GameHistory = gameHistory
                        };

                        userProfiles.Add(userProfile);
                    }
                }
                return userProfiles;
            }
            catch (Exception)
            {
                throw new Exception("Error al convertir los perfiles de usuario");
            }
        }
        #endregion

        public async Task<List<RecomendacionVideojuegoModel>> ObtenerRecomendacionesVisitas(string Usuario_ID)
        {
            try
            {
                // Obtiene las recomendaciones filtrando por tipo "Contenido"
                var recomendaciones = (await _recomendacionVideojuegoRepository.Get(
                    filter: r => r.TipoRecomendacion == "Contenido" && r.UserId == Usuario_ID,
                    includeProperties: "videojuegoReferencia,videojuegoRecomendado"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por FechaRecomendacion descendente
                    .ToList();

                // Agrupa por VideojuegoReferenciaId, ordena los bloques y toma los 3 últimos
                var ultimosBloques = recomendaciones
                    .GroupBy(r => r.VideojuegoReferenciaId)
                    .OrderByDescending(g => g.Max(r => r.FechaRecomendacion)) // Ordenar bloques por la fecha más reciente en cada grupo
                    .Take(3) // Tomar los 3 últimos bloques
                    .SelectMany(g => g) // Aplanar los grupos en una lista
                    .ToList();

                return ultimosBloques;
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones por visitas del usuario {Usuario_ID}");
            }
        }
        public async Task<List<RecomendacionVideojuegoModel>> ObtenerRecomendacionesForosFav(string Usuario_ID)
        {
            try
            {
                // Obtiene las recomendaciones filtrando por tipo "Contenido"
                var recomendaciones = (await _recomendacionVideojuegoRepository.Get(
                    filter: r => r.TipoRecomendacion == "Foro" && r.UserId == Usuario_ID,
                    includeProperties: "videojuegoReferencia,videojuegoRecomendado"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por FechaRecomendacion descendente
                    .ToList();

                // Agrupa por VideojuegoReferenciaId, ordena los bloques y toma los 3 últimos
                var ultimosBloques = recomendaciones
                    .GroupBy(r => r.VideojuegoReferenciaId)
                    .OrderByDescending(g => g.Max(r => r.FechaRecomendacion)) // Ordenar bloques por la fecha más reciente en cada grupo
                    .Take(3) // Tomar los 3 últimos bloques
                    .SelectMany(g => g) // Aplanar los grupos en una lista
                    .ToList();

                return ultimosBloques;
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones por favoritos del usuario {Usuario_ID}");
            }
        }
        public async Task<List<RecomendacionUsuarioModel>> ObtenerRecomendacionesColabVisitas(string Usuario_ID)
        {
            try
            {
                // Obtener las últimas 10 recomendaciones de tipo "Visita"
                var recomendacionesVisita = (await _recomendacionUsuarioRepository.Get(
                    filter: r => r.UserId == Usuario_ID && r.TipoRecomendacion == "Visita",
                    includeProperties: "videojuego"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por fecha descendente
                    .Take(10) // Tomar los últimos 10
                    .ToList();

                return (recomendacionesVisita);
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones colaborativas por visitas del usuario {Usuario_ID}");
            }
        }
        public async Task<List<RecomendacionUsuarioModel>> ObtenerRecomendacionesColabForosFav(string Usuario_ID)
        {
            try
            {
                // Obtener las últimas 10 recomendaciones de tipo "ForoFav"
                var recomendacionesForoFav = (await _recomendacionUsuarioRepository.Get(
                    filter: r => r.UserId == Usuario_ID && r.TipoRecomendacion == "ForoFav",
                    includeProperties: "videojuego"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por fecha descendente
                    .Take(10) // Tomar los últimos 10
                    .ToList();

                return (recomendacionesForoFav);
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones colaborativas por favoritos del usuario {Usuario_ID}");
            }
        }
        public async Task<List<ForoModel>> ObtenerForosRecPorVisitas(string Usuario_ID)
        {
            try
            {
                // Obtiene las recomendaciones filtrando por tipo "Contenido"
                var recomendaciones = (await _recomendacionVideojuegoRepository.Get(
                    filter: r => r.TipoRecomendacion == "Contenido" && r.UserId == Usuario_ID,
                    includeProperties: "videojuegoReferencia,videojuegoRecomendado"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por FechaRecomendacion descendente
                    .ToList();

                // Agrupa por VideojuegoReferenciaId, ordena los bloques y toma los 3 últimos
                var ultimoBloque = recomendaciones
                    .GroupBy(r => r.VideojuegoReferenciaId)
                    .OrderByDescending(g => g.Max(r => r.FechaRecomendacion)) // Ordenar bloques por la fecha más reciente en cada grupo
                    .Take(1) // Tomar los 3 últimos bloques
                    .SelectMany(g => g) // Aplanar los grupos en una lista
                    .ToList();

                List<ForoModel> Foros = new List<ForoModel>();

                foreach (var item in ultimoBloque)
                {
                    var result = (await _foroRepository.Get(filter: r => r.Videojuego_ID == item.VideojuegoRecomendadoId, includeProperties: "foroUsuarioFavoritoModels, foroUsuarioVisitaModels, comentarioModels, videojuego,usuario")).OrderByDescending(x => x.foroUsuarioVisitaModels.Count).FirstOrDefault();
                    if (result!=null)
                    {
                        Foros.Add(result);
                    }
                }
                return Foros;
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones de foros por visitas del usuario {Usuario_ID}");
            }
        }
        public async Task<List<ForoModel>> ObtenerForosRecPorFavoritos(string Usuario_ID)
        {
            try
            {
                // Obtiene las recomendaciones filtrando por tipo "Contenido"
                var recomendaciones = (await _recomendacionVideojuegoRepository.Get(
                    filter: r => r.TipoRecomendacion == "Foro" && r.UserId == Usuario_ID,
                    includeProperties: "videojuegoReferencia,videojuegoRecomendado"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por FechaRecomendacion descendente
                    .ToList();

                // Agrupa por VideojuegoReferenciaId, ordena los bloques y toma los 3 últimos
                var ultimoBloque = recomendaciones
                    .GroupBy(r => r.VideojuegoReferenciaId)
                    .OrderByDescending(g => g.Max(r => r.FechaRecomendacion)) // Ordenar bloques por la fecha más reciente en cada grupo
                    .Take(1) // Tomar los 3 últimos bloques
                    .SelectMany(g => g) // Aplanar los grupos en una lista
                    .ToList();

                List<ForoModel> Foros = new List<ForoModel>();

                foreach (var item in ultimoBloque)
                {
                    var result = (await _foroRepository.Get(filter: r => r.Videojuego_ID == item.VideojuegoRecomendadoId, includeProperties: "foroUsuarioFavoritoModels, foroUsuarioVisitaModels, comentarioModels, videojuego,usuario")).OrderByDescending(x => x.foroUsuarioVisitaModels.Count).FirstOrDefault();
                    if (result != null)
                    {
                        Foros.Add(result);
                    }
                }
                return Foros;
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones de foros por favoritos del usuario {Usuario_ID}");
            }
        }
        public async Task<List<ForoModel>> ObtenerForosRecColaborativos(string Usuario_ID)
        {
            try
            {
                // Obtener las últimas 10 recomendaciones de tipo "Visita"
                var recomendacionesColab = (await _recomendacionUsuarioRepository.Get(
                    filter: r => r.UserId == Usuario_ID,
                    includeProperties: "videojuego"))
                    .OrderByDescending(r => r.FechaRecomendacion) // Ordenar por fecha descendente
                    .Take(10) // Tomar los últimos 10
                    .ToList();

                List<ForoModel> Foros = new List<ForoModel>();

                foreach (var item in recomendacionesColab)
                {
                    var result = (await _foroRepository.Get(filter: r => r.Videojuego_ID == item.VideojuegoRecomendadoId, includeProperties: "foroUsuarioFavoritoModels, foroUsuarioVisitaModels, comentarioModels, videojuego,usuario")).OrderByDescending(x => x.foroUsuarioVisitaModels.Count).FirstOrDefault();
                    if (result != null)
                    {
                        Foros.Add(result);
                    }
                }
                return Foros;
            }
            catch (Exception)
            {
                throw new Exception($"Error obtener recomendaciones de foros colaborativas del usuario {Usuario_ID}");
            }
        }
    }
}  