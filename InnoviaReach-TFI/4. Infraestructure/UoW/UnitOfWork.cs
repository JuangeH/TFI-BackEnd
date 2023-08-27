using Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transversal.Extensions;

namespace Infrastructure.Data.UoW
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        private List<Type> _repositories;

        public UnitOfWork(ApplicationDbContext context) : base(context)
        {
            InitializeRepositories();
        }

        public I GetRepository<I>()
        {
            var repository = _repositories.FirstOrDefault(t => typeof(I).IsAssignableFrom(t));
            return (I)Activator.CreateInstance(repository, Context);
        }

        private void InitializeRepositories()
        {
            _repositories = new List<Type>();
            var genericType = typeof(IGenericRepository<>).GetGenericTypeDefinition();
            foreach (var iRepository in genericType.Assembly.GetTypes(t => t.IsInterface && t.ImplementsGenericInterface(genericType)))
            {
                var repository = this.GetType().Assembly.FindType(t => t.IsClass && iRepository.IsAssignableFrom(t));
                _repositories.Add(repository);
            }
        }
    }
}
