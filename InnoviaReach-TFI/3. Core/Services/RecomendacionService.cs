using AutoMapper;
using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class RecomendacionService : GenericService<RecomendacionModel>, IRecomendacionService
    {
        private MLContext mlContext = new MLContext();
        
        private IVideojuegoRepository _videojuegoRepository;
        private IUsersRepository _usersRepository;
        private IRecomendacionVideojuegoRepository _recomendacionVideojuegoRepository;
        private ITagRepository _tagRepository;
        private IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public RecomendacionService(IUnitOfWork unitOfWork, IMapper mapper)
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
        }

        #region VIEJO

        private IForoRepository _foroRepository;
        private IForoUsuarioVisitaRepository _foroUsuarioVisitaRepository;

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
        // Clase auxiliar para almacenar el resultado del clustering
        public class PredictedCluster
        {
            public uint PredictedLabel { get; set; } // Cambiar de int a uint
            [VectorType] public float[] CaracteristicasVector { get; set; }
            public float[] ReducedFeatures { get; set; }
        }

        public class PredictedUserCluster
        {
            public uint PredictedLabel { get; set; }
            [VectorType] public float[] Features { get; set; }
        }
        //private int EncontrarMejorNumeroDeClusters(IDataView data, string featuresColumnName, int minK, int maxK)
        //{
        //    int mejorK = minK;
        //    double mejorSSE = double.MaxValue;

        //    for (int k = minK; k <= maxK; k++)
        //    {
        //        var pipeline = mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: k);
        //        var model = pipeline.Fit(data);
        //        var predictions = model.Transform(data);

        //        // Evaluar el modelo para calcular la suma de errores cuadráticos (SSE)
        //        var metrics = mlContext.Clustering.Evaluate(predictions);
        //        double currentSSE = metrics.AverageDistance;

        //        if (currentSSE < mejorSSE)
        //        {
        //            mejorSSE = currentSSE;
        //            mejorK = k;
        //        }
        //    }

        //    return mejorK;
        //}

        #endregion

        #region RECOMENDACIONES VIDEOJUEGOS

        public async Task CrearClusters(List<VideojuegoClusterModel> juegos)
        {
            // 1. Preparar los datos
            var data = mlContext.Data.LoadFromEnumerable(juegos);

            // 2. Transformar texto a vector de características y aplicar reducción de dimensionalidad (PCA)
            int sqrtCount = (int)Math.Sqrt(juegos.Count);
            int pcaDimensions = Math.Min(50, sqrtCount * 2); // Elige la cantidad de dimensiones a reducir, ajusta según necesidad

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
                juegos[i].CaracteristicasVector = clusters[i].ReducedFeatures; // Usa ReducedFeatures en lugar de CaracteristicasVector
            }

            //5.Actualizar la base de datos
            foreach (var item in juegos)
            {
                var videojuego = (await _videojuegoRepository.Get(x => x.AppRawgId == item.AppRawgId)).FirstOrDefault();

                videojuego.ClusterID = item.ClusterID;
                videojuego.CaracteristicasVector = JsonConvert.SerializeObject(item.CaracteristicasVector);

                await _videojuegoRepository.Update(videojuego);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        // Método para calcular similitud de coseno
        public float CalcularSimilitudCoseno(float[] vectorA, float[] vectorB)
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

        // Método para generar recomendaciones
        public async Task GenerarRecomendaciones(VideojuegoModel juegoReferencia, string Usuario_ID)
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

            foreach (var item in recomendaciones)
            {
                RecomendacionVideojuegoModel recomendacionVideojuegoModel = new RecomendacionVideojuegoModel();

                recomendacionVideojuegoModel.FechaRecomendacion = DateTime.Now;
                recomendacionVideojuegoModel.VideojuegoReferenciaId = juegoReferencia.Videojuego_ID;
                recomendacionVideojuegoModel.VideojuegoRecomendadoId = item.Videojuego_ID;
                recomendacionVideojuegoModel.UserId = Usuario_ID;
                recomendacionVideojuegoModel.Similitud = item.Similitud;

                var vidjuego = await _recomendacionVideojuegoRepository.Get(x => x.VideojuegoReferenciaId == juegoReferencia.Videojuego_ID);

                if (vidjuego is null)
                {
                    await _recomendacionVideojuegoRepository.Insert(recomendacionVideojuegoModel);
                }
                else
                {
                    await _recomendacionVideojuegoRepository.Delete(vidjuego);
                    await _recomendacionVideojuegoRepository.Insert(recomendacionVideojuegoModel);
                }

                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region RECOMENDACIONES ACTIVIDAD-USUARIO

        public async Task CrearClustersUsuarios()
        {
            var users = (await _usersRepository.Get(
                includeProperties: "usuarioVisitaModels, usuarioVisitaModels.Videojuego, usuarioVisitaModels.Videojuego.videojuegoGeneroModels.generoModel, usuarioVisitaModels.Videojuego.videojuegoTagModels.tagModel")).ToList();

            var perfilesUsuarios = ConvertirAUserProfiles(users);
            int sqrtCount = (int)Math.Sqrt(perfilesUsuarios.Count);

            // Definir el vocabulario de géneros y tags
            List<string> vocabularioGeneros = new List<string>();
            List<string> vocabularioTags = new List<string>();

            var generos = await _generoRepository.Get();
            vocabularioGeneros = generos.Select(g => g.Nombre).Distinct().ToList();

            var tags = await _tagRepository.Get();
            vocabularioTags = tags.Select(g => g.Nombre).Distinct().ToList();

            int longitudGeneros = vocabularioGeneros.Count;
            int longitudTags = vocabularioTags.Count;
            int longitudTotal = longitudGeneros + longitudTags;

            var schemaDefinition = SchemaDefinition.Create(typeof(UsuarioClusterDataModel));
            schemaDefinition["GenresVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudGeneros);
            schemaDefinition["TagsVector"].ColumnType = new VectorDataViewType(NumberDataViewType.Single, longitudTags);


            // Transformar los perfiles
            TransformarPerfiles(perfilesUsuarios, vocabularioGeneros, vocabularioTags);

            // Preparar los datos para ML.NET
            var datosML = PrepararDatosParaML(perfilesUsuarios, longitudGeneros, longitudTags);
            var data = mlContext.Data.LoadFromEnumerable(datosML, schemaDefinition);

            var pipeline = mlContext.Transforms
                .Concatenate("Features", "GenresVector", "TagsVector")
                .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: sqrtCount));

            var model = pipeline.Fit(data);
            var predictions = model.Transform(data);

            // Asignar el ClusterID a cada perfil de usuario
            var clusters = mlContext.Data.CreateEnumerable<PredictedUserCluster>(predictions, reuseRowObject: false).ToList();
            for (int i = 0; i < perfilesUsuarios.Count; i++)
            {
                perfilesUsuarios[i].ClusterID = (int)clusters[i].PredictedLabel;
            }

            foreach (var perfil in perfilesUsuarios)
            {
                // Obtener el usuario de la base de datos
                var usuario = (await _usersRepository.Get(u => u.Id == perfil.UserId)).FirstOrDefault();

                if (usuario != null)
                {
                    // Actualizar el ClusterID
                    usuario.ClusterID = perfil.ClusterID;

                    // Serializar GameGenres, GameTags y GameHistory a formato JSON
                    usuario.GameGenresJson = JsonConvert.SerializeObject(perfil.GameGenres);
                    usuario.GameTagsJson = JsonConvert.SerializeObject(perfil.GameTags);
                    usuario.GameHistoryJson = JsonConvert.SerializeObject(perfil.GameHistory);

                    // Actualizar el usuario en la base de datos
                    _usersRepository.Update(usuario);
                }
            }

            // Guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
        }

        private float CalcularSimilitudCosenoUsuario(Dictionary<string, int> vectorA, Dictionary<string, int> vectorB)
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
        public async Task<List<string>> GenerarRecomendacionesColaborativas(string userId)
        {
            // Obtener el usuario objetivo desde la base de datos
            var user = (await _usersRepository.Get(u => u.Id == userId)).FirstOrDefault();

            if (user == null)
            {
                throw new Exception($"Usuario con ID {userId} no encontrado.");
            }

            // Deserializar los datos del perfil del usuario
            var perfilUsuario = new UsuarioClusterModel
            {
                UserId = user.Id,
                ClusterID = user.ClusterID ?? -1,
                GameGenres = string.IsNullOrEmpty(user.GameGenresJson)
                    ? new Dictionary<string, int>()
                    : JsonConvert.DeserializeObject<Dictionary<string, int>>(user.GameGenresJson),
                GameTags = string.IsNullOrEmpty(user.GameTagsJson)
                    ? new Dictionary<string, int>()
                    : JsonConvert.DeserializeObject<Dictionary<string, int>>(user.GameTagsJson),
                GameHistory = string.IsNullOrEmpty(user.GameHistoryJson)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(user.GameHistoryJson)
            };

            // Obtener todos los usuarios del mismo cluster
            var users = await _usersRepository.Get(filter: u => u.ClusterID == perfilUsuario.ClusterID && u.Id != userId);

            // Crear perfiles de usuario a partir del JSON
            var userProfiles = users.Select(u => new UsuarioClusterModel
            {
                UserId = u.Id,
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

            foreach (var usuarioSimilar in usuariosSimilares.Take(5)) // Limitar a los 5 usuarios más similares
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
            return recomendaciones.OrderByDescending(r => r.Value).Select(r => r.Key).Take(5).ToList();
        }
        public void TransformarPerfiles(List<UsuarioClusterModel> perfilesUsuarios, List<string> vocabularioGeneros, List<string> vocabularioTags)
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
        public List<UsuarioClusterDataModel> PrepararDatosParaML(List<UsuarioClusterModel> perfilesUsuarios, int longitudGeneros, int longitudTags)
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


        public List<UsuarioClusterModel> ConvertirAUserProfiles(List<Users> users)
        {
            var userProfiles = new List<UsuarioClusterModel>();

            foreach (var user in users)
            {
                // Inicializar diccionarios para géneros y tags
                var gameGenres = new Dictionary<string, int>();
                var gameTags = new Dictionary<string, int>();
                var gameHistory = new List<string>();

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

            return userProfiles;
        }


        //private UsuarioClusterModel ConstruirDatosUsuario(Users user, List<string> genreKeys, List<string> tagKeys)
        //{
        //    var gameGenres = new Dictionary<string, float>();
        //    var gameTags = new Dictionary<string, float>();
        //    var gameHistory = new List<string>();

        //    foreach (var visita in user.usuarioVisitaModels)
        //    {
        //        var videojuego = visita.Videojuego;

        //        // Agregar al historial de juegos
        //        if (!gameHistory.Contains(videojuego.Nombre))
        //        {
        //            gameHistory.Add(videojuego.Nombre);
        //        }

        //        // Contabilizar géneros
        //        foreach (var genero in videojuego.videojuegoGeneroModels)
        //        {
        //            var nombreGenero = genero.generoModel.Nombre;
        //            if (gameGenres.ContainsKey(nombreGenero))
        //                gameGenres[nombreGenero]++;
        //            else
        //                gameGenres[nombreGenero] = 1;
        //        }

        //        // Contabilizar etiquetas
        //        foreach (var tag in videojuego.videojuegoTagModels)
        //        {
        //            var nombreTag = tag.tagModel.Nombre;
        //            if (gameTags.ContainsKey(nombreTag))
        //                gameTags[nombreTag]++;
        //            else
        //                gameTags[nombreTag] = 1;
        //        }
        //    }

        //    // Convertir diccionarios a vectores
        //    var gameGenresVector = ConvertToVector(gameGenres, genreKeys);
        //    var gameTagsVector = ConvertToVector(gameTags, tagKeys);

        //    return new UsuarioClusterModel
        //    {
        //        UserId = user.Id,
        //        GameGenresVector = gameGenresVector,
        //        GameTagsVector = gameTagsVector,
        //        GameHistory = gameHistory
        //    };
        //}
        //private float[] ConvertToVector(Dictionary<string, float> data, List<string> keys)
        //{
        //    var vector = new float[keys.Count];
        //    for (int i = 0; i < keys.Count; i++)
        //    {
        //        vector[i] = data.ContainsKey(keys[i]) ? data[keys[i]] : 0;
        //    }
        //    return vector;
        //}



        #endregion

    }

}