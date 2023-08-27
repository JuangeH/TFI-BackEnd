using System;
using System.Collections.Generic;
using System.Text;
using Transversal.StorageService.Configurations;
using Transversal.StorageService.Contracts;

namespace Transversal.StorageService.Factory
{
    public class GenericStorageServiceFactory : IGenericStorageServiceFactory
    {
        private readonly IFileSystemStorageService _fileSystemStorageService;
        private readonly IGoogleCloudStorageService _googlecloudStorageService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly GenericStorageConfiguration _storageConfiguration;

        private readonly Dictionary<string, GenericStorageTypeEnum> _types;

        public GenericStorageServiceFactory(
            IFileSystemStorageService fileSystemStorageService,
            IGoogleCloudStorageService cloudStorageService,
            IAzureBlobStorageService AzureBlobStorageService,
            GenericStorageConfiguration storageConfiguration)
        {
            _fileSystemStorageService = fileSystemStorageService;
            _googlecloudStorageService = cloudStorageService;
            _azureBlobStorageService = AzureBlobStorageService;
            _storageConfiguration = storageConfiguration;

            _types = new Dictionary<string, GenericStorageTypeEnum>
            {
                { GenericStorageTypeEnum.FSS.ToString(), GenericStorageTypeEnum.FSS },
                { GenericStorageTypeEnum.GCS.ToString(), GenericStorageTypeEnum.GCS },
                { GenericStorageTypeEnum.ABS.ToString(), GenericStorageTypeEnum.ABS }
            };
        }

        public IGenericStorageService GetDefault() => Get(_types[_storageConfiguration.Type]); //Por defecto nos devuelve el que tenemos en el appsettings

        public IGenericStorageService Get(GenericStorageTypeEnum storageType) => storageType switch //Usamos este método solo si necesitamos mas de un servicio.
        {
            GenericStorageTypeEnum.FSS => _fileSystemStorageService,
            GenericStorageTypeEnum.GCS => _googlecloudStorageService,
            GenericStorageTypeEnum.ABS => _azureBlobStorageService,
            _ => null,
        };

        public IGenericStorageService Get(string storageType) => Get(_types[storageType]);
    }
}
