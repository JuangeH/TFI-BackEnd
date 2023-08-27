using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Repositories
{
    public partial interface IGenericRepository<T> : IDisposable where T : class
    {

        public T GetById(object id);

        public Task<T> GetByIdAsync(object id);

        public Task Insert(T entity);

        public Task Insert(IEnumerable<T> entities);

        public Task Update(T entity);

        public Task Update(IEnumerable<T> entities);

        public Task Delete(T entity);

        public Task Delete(IEnumerable<T> entities);

        public Task CancelChanges(T entity);

        /// <summary>
        /// Devuelve una lista acorde al filtro y orden especificados. La ejecución del
        /// query sobre la base de datos depende de la acción que se realice sobre la lista
        /// que devuelve este método (para más información ver: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/classification-of-standard-query-operators-by-manner-of-execution
        /// </summary>
        /// <param name="filter">Predicado para filtrar los registros</param>
        /// <param name="orderBy">Indica de qué manera se deben ordenar los registros</param>
        /// <param name="includeProperties">Propiedades a incluir, esto genera operaciones join con otras tablas</param>
        /// <param name="ignoreQueryFilters">Se usa para ignorar filtros por defecto en la configuración del DBContext</param>
        /// <returns>Un IEnumerable de elementos del tipo T</returns>
        public Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool ignoreQueryFilters = false,
            bool tracking = true);

        /// <summary>
        /// Busca un registro acorde al filtro y orden especificados.
        /// </summary>
        /// <param name="filter">Predicado para filtrar los registros</param>
        /// <param name="orderBy">Indica de qué manera se deben ordenar los registros</param>
        /// <param name="includeProperties">Propiedades a incluir, esto genera operaciones join con otras tablas</param>
        /// <param name="ignoreQueryFilters">Se usa para ignorar filtros por defecto en la configuración del DBContext</param>
        /// <returns>El primer elemento que se encuentre, null si no hay resultados</returns>
        public Task<T> GetOne(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            bool ignoreQueryFilters = false,
            bool tracking = true);

        public IEnumerable<T> GetPagedElements<S>(
            int pageIndex,
            int pageCount,
            Expression<Func<T, S>> orderByExpression,
            bool ascending,
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "");

        public IQueryable<T> Table { get; }

        public IQueryable<T> TableNoTracking { get; }
    }
}
