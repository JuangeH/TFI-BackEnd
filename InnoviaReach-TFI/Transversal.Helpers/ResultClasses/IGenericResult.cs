using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.Helpers.ResultClasses
{
    public interface IGenericResult
    {
        /// <summary>
        /// Mensaje general descriptivo del resultado de la operación
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Listado de errores encontrados
        /// </summary>
        public IList<string> Errors { get; set; }

        /// <summary>
        /// Indica si el resultado es satisfactorio
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Listado de inconvenientes/advertencias, no refiere a errores criticos
        /// </summary>
        public IList<string> Issues { get; set; }

        void AddError(string error);

        void AddIssue(string issue);
    }

    public interface IGenericResult<T> : IGenericResult where T : class
    {
        public T Data { get; set; }

        public bool HasValue { get; }
    }
}
