using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.StorageService.Contracts
{
    public interface IGenericStorageService
    {
        Task<GenericStoreResult> Store(string filename, string contentFile);
        Task<string> Restore(string volume, string fileFullPath);
        void Delete(string volume, string fileFullPath);
    }
}
