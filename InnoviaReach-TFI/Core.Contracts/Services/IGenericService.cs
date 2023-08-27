using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Services
{
    public partial interface IGenericService<T> where T : class
    {
        public Task CreateAsync(T entity);

        public Task<bool> CreateAsync(IEnumerable<T> entities);

        public Task DeleteAsync(object id);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(object id);

        public Task UpdateAsync(T entity);

        public IEnumerable<T> GetPagedElements<S>(
            int pageIndex,
            int pageCount,
            Expression<Func<T, S>> orderByExpression,
            bool ascending,
            Expression<Func<T, bool>> filter = null);
    }
}