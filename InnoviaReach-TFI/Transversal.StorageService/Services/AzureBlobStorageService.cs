using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Transversal.StorageService.Configurations;
using Transversal.StorageService.Contracts;

namespace Transversal.StorageService.Services
{
    public class AzureBlobStorageService : GenericStorageService, IAzureBlobStorageService
    {
        private readonly AzureBlobStorageConfiguration _azureBlobStorageConfiguration;
        private readonly ILogger<AzureBlobStorageService> _logger;

        public AzureBlobStorageService(
            AzureBlobStorageConfiguration AzureBlobStorageConfiguration,
            ILogger<AzureBlobStorageService> logger
            )
        {
            _azureBlobStorageConfiguration = AzureBlobStorageConfiguration;
            _logger = logger;
        }

        public async override Task<GenericStoreResult> Store(string filename, string contentFile)
        {
            GenericStoreResult result = new GenericStoreResult();

            string storageConnection = _azureBlobStorageConfiguration.StorageConnectionString;
            string containerName = _azureBlobStorageConfiguration.ContainerName;
            BlobContainerClient container = new BlobContainerClient(storageConnection, containerName);
            if (!container.Exists())
                await container.CreateAsync(publicAccessType: PublicAccessType.Blob);
            try
            {
                var bytes = Convert.FromBase64String(contentFile);
                var fileStream = new MemoryStream(bytes);
                BlobClient blob = container.GetBlobClient(filename);
                BlobContentInfo resultUpload = await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentFile });

                result.Result = 0;
                result.Volume = "";
                result.FullPath = filename;
                result.StorageType = GenericStorageTypeEnum.ABS.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "In Store: " + ex.Message);
                throw ex;
            }
            
            return result;
        }
        
        public override async Task<string> Restore(string volume, string fileFullPath)
        {
            byte[] filebytes = null;
            try
            {
                string storageConnection = _azureBlobStorageConfiguration.StorageConnectionString;
                string containerName = _azureBlobStorageConfiguration.ContainerName;

                BlobContainerClient containerClient = new BlobContainerClient(storageConnection, containerName);
                BlobClient blobClient = containerClient.GetBlobClient(fileFullPath);
                if ((await blobClient.ExistsAsync()))
                {
                    using (var ms = new MemoryStream())
                    {
                        blobClient.DownloadTo(ms);
                        filebytes = ms.ToArray();
                    }
                }
                else
                {
                    _logger.LogInformation($"the root: {fileFullPath} not found in azure blob storage.");
                    throw new Exception("File not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "In Delete: " + ex.Message);
                throw ex;
            }
            
            return Convert.ToBase64String(filebytes);
        }

        public async override void Delete(string volume, string fileFullPath)
        {
            try
            {
                string storageConnection = _azureBlobStorageConfiguration.StorageConnectionString;
                string containerName = _azureBlobStorageConfiguration.ContainerName;
                BlobContainerClient container = new BlobContainerClient(storageConnection, containerName);

                var response = await container.DeleteBlobIfExistsAsync(fileFullPath, DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "In Delete: " + ex.Message);
                throw ex;
            }
        }
    }
}
