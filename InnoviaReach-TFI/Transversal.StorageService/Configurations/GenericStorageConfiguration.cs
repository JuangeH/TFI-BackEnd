using System;
using System.Collections.Generic;
using System.Text;

namespace Transversal.StorageService.Configurations
{
    public class GenericStorageConfiguration
    {
        public string Type { get; set; }
        public FileSystemStorageConfiguration FileSystemStorage { get; set; }
        public GoogleCloudStorageConfiguration GoogleCloudStorage { get; set; }
        public AzureBlobStorageConfiguration AzureBlobStorage { get; set; }
    }
}
