using Core.Domain.ApplicationModels;
using Core.Domain.Models;
using Core.Domain.Models.Nueva_Base;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public interface IVideojuegoService : IGenericService<VideojuegoModel>
    {
        public Task<List<VideojuegoModel>> ObtenerVideojuegos();
        public Task<VideojuegoModel> ObtenerVideojuego(int SteamAppid);
        public Task<VideojuegoModel> RegistrarObtenerVideojuego(VideojuegoRAWG videojuego);
        public Task RegistrarVideojuegoEstiloGenero(VideojuegoModel videojuego, List<RatingModel> ratings, List<GeneroModel> generos, List<TiendaModel> tiendas, List<TagModel> tags, List<PlataformaModel> plataformaModels);
        public Task AgregarDescripcion();
        public Task<(List<VideojuegoModel> Videojuegos, int TotalRecords)> ObtenerVideojuegosCatalogo(int pageNumber, int pageSize);
        public Task<List<VideojuegoModel>> ObtenerVideojuegosForo();
        public Task<List<VideojuegoModel>> BuscarVideojuegosForo(string nombre, int pageSize);
        public Task<VideojuegoModel> ObtenerVideojuegoPorNombre(string nombre);

    }
}
