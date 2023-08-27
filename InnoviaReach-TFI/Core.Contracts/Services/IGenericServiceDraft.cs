using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3._Core.Contracts
{
    public interface IGenericServiceDraft<T>
    {
        void CancelarCambios(T elemento);
        void Eliminar(T elemento);
        List<T> Listar();
        T ListarById(T elemento);
        T ListarOne(T elemento);
        List<T> ListarTodosById(T elemento);
        void Modificar(T elemento);
        void Registrar(T elemento);
    }
}
