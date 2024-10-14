using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class RecomendacionesService : IRecomendacionesService
    {
        private IForoRepository _foroRepository;
        private IForoUsuarioVisitaRepository _foroUsuarioVisitaRepository;
        // Diccionario donde se almacena el Estilo_ID, Nombre_Estilo y la cantidad de veces que aparece
        Dictionary<int, Tuple<string, int>> keyValuePairsEstilos = new Dictionary<int, Tuple<string, int>>();
        Dictionary<int, Tuple<string, int>> keyValuePairsGeneros = new Dictionary<int, Tuple<string, int>>();


        public RecomendacionesService(IForoRepository foroRepository, IForoUsuarioVisitaRepository foroUsuarioVisitaRepository)
        {
            _foroRepository = foroRepository;
            _foroUsuarioVisitaRepository = foroUsuarioVisitaRepository;
        }

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
                    keyValuePairsGeneros[item2.Genero_ID] = new Tuple<string, int>(item2.generoModel.Descripcion, 1);
                }
            }

            //// Aquí puedes hacer algo con el diccionario, como obtener el estilo más repetido
            //// Esto es solo un ejemplo de cómo podrías obtener el más repetido:
            //var topEstilo = keyValuePairsEstilos.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();

            //// Retornar el ID del estilo más repetido o hacer lo que necesites
            //return topEstilo.Key;
        }
    }
}