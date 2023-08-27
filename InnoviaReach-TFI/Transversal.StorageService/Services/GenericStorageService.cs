using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transversal.StorageService.Contracts;

namespace Transversal.StorageService.Services
{
    public abstract class GenericStorageService : IGenericStorageService
    {
        public abstract Task<GenericStoreResult> Store(string filename, string contentFile);
        public abstract Task<string> Restore(string volume, string fileFullPath);
        public abstract void Delete(string volume, string fileFullPath);
    }
}
