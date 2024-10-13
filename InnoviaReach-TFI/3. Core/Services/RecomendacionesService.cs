using Core.Business.Services;
using Core.Contracts.Repositories;
using Core.Contracts.Services;
using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Services
{
    public class RecomendacionesService : IRecomendacionesService
    {
        private IForoRepository _foroRepository;
        private IForoUsuarioVisitaRepository _foroUsuarioVisitaRepository;
        // Diccionario donde se almacena el Estilo_ID, Nombre_Estilo y la cantidad de veces que aparece
        Dictionary<int, Tuple<string, int>> keyValuePairs = new Dictionary<int, Tuple<string, int>>();

        public RecomendacionesService(IForoRepository foroRepository, IForoUsuarioVisitaRepository foroUsuarioVisitaRepository)
        {
            _foroRepository = foroRepository;
            _foroUsuarioVisitaRepository = foroUsuarioVisitaRepository;
        }

        public async Task<List<ForoModel>> RecomendacionesPorVisitas(string User_ID)
        {
            try
            {
                var foroUsuarioVisitaList = (await _foroUsuarioVisitaRepository.Get(x => x.User_ID == User_ID, includeProperties: "foro,foro.videojuego,foro.videojuego.videojuegoEstiloModels,foro.videojuego.videojuegoGeneroModels,foro.videojuego.videojuegoEstiloModels.estiloModel")).ToList();

                foreach (var item in foroUsuarioVisitaList)
                {
                    var result = ObtenerTopEstilo(item.foro.videojuego.videojuegoEstiloModels.ToList());
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int ObtenerTopEstilo(List<VideojuegoEstiloModel> estilos)
        {
            foreach (var item2 in estilos)
            {
                // Verificar si el Estilo_ID ya está en el diccionario
                if (keyValuePairs.ContainsKey(item2.Estilo_ID))
                {
                    // Si está, actualizamos la cantidad
                    var currentTuple = keyValuePairs[item2.Estilo_ID];
                    keyValuePairs[item2.Estilo_ID] = new Tuple<string, int>(currentTuple.Item1, currentTuple.Item2 + 1);
                }
                else
                {
                    // Si no está, lo añadimos con cantidad 1
                    keyValuePairs[item2.Estilo_ID] = new Tuple<string, int>(item2.estiloModel.Descripcion, 1);
                }
            }

            // Aquí puedes hacer algo con el diccionario, como obtener el estilo más repetido
            // Esto es solo un ejemplo de cómo podrías obtener el más repetido:
            var topEstilo = keyValuePairs.OrderByDescending(kv => kv.Value.Item2).FirstOrDefault();

            // Retornar el ID del estilo más repetido o hacer lo que necesites
            return topEstilo.Key;
        }
    }
}