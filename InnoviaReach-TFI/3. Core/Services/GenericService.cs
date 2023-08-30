using Core.Contracts.Repositories;
using Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.Services
{
    public abstract class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork,
            IGenericRepository<T> repository)
        {
            this._unitOfWork = unitOfWork;
            this._repository = repository;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _repository.Insert(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.Update(entity);
        }

        public virtual async Task DeleteAsync(object id)
        {
            T entity = await _repository.GetByIdAsync(id);
            await _repository.Delete(entity);
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.Run(() => _repository.TableNoTracking.AsEnumerable<T>());
        }

        protected async Task<T> GetByIdAsync(Expression<Func<T, bool>> func)
        {
            var result = (await _repository.Get(func)).FirstOrDefault();

            return result;
        }

        public virtual IEnumerable<T> GetPagedElements<S>(
            int pageIndex,
            int pageCount,
            Expression<Func<T, S>> orderByExpression,
            bool ascending = true,
            Expression<Func<T, bool>> filter = null)
        {
            return _repository.GetPagedElements(pageIndex, pageCount, orderByExpression, ascending, filter);
        }

        public async Task<bool> CreateAsync(IEnumerable<T> entities)
        {
            var result = true;
            try
            {
                await _repository.Insert(entities);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
