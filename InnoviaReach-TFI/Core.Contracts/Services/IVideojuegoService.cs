using Core.Domain.ApplicationModels;
using Core.Domain.Models;
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
        public Task<VideojuegoModel> ObtenerVideojuego(string name);
        public Task RegistrarVideojuego(VideojuegoModel videojuego, JArray categoriesArray, JArray genresArray);
        public Task<(List<VideojuegoModel> Videojuegos, int TotalRecords)> ObtenerVideojuegosCatalogo(int pageNumber, int pageSize);
    }
}
