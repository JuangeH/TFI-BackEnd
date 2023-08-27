using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.StorageService.Configurations
{
    public class AzureBlobStorageConfiguration
    {
        public string BaseUrl { get; set; }
        public string StorageConnectionString { get; set; }
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string ContainerName { get; set; }
    }
}
