using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._Infraestructure.Contracts
{
    public interface IGenericRepository<T>
    {
        void CancelChanges (T elemento);
        void Delete (T elemento);
        List<T> GetAll();
        T GetAllById(T elemento);
        T GetById(T elemento);
        List<T> GetOne(T elemento);
        void Update(T elemento);
        void Insert(T elemento);
    }
}
