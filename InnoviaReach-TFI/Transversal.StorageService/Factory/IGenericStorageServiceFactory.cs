using System;
using System.Collections.Generic;
using System.Text;
using Transversal.StorageService.Contracts;

namespace Transversal.StorageService.Factory
{
    public interface IGenericStorageServiceFactory
    {
        IGenericStorageService GetDefault();

        IGenericStorageService Get(GenericStorageTypeEnum storageType);

        IGenericStorageService Get(string storageType);
    }
}
