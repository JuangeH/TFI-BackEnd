using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Transversal.StorageService.Configurations;
using Transversal.StorageService.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Transversal.StorageService.Services
{
    public class GoogleCloudStorageService : GenericStorageService, IGoogleCloudStorageService
    {
        private readonly GoogleCloudStorageConfiguration _GoogleCloudStorageConfiguration;
        private readonly ILogger<GoogleCloudStorageService> _logger;
        private readonly IHostingEnvironment _environment;

        public GoogleCloudStorageService(
            GoogleCloudStorageConfiguration GoogleCloudStorageConfiguration,
            ILogger<GoogleCloudStorageService> logger,
            IHostingEnvironment environment
            )
        {
            _GoogleCloudStorageConfiguration = GoogleCloudStorageConfiguration;
            _logger = logger;
            _environment = environment;
        }

        public override async Task<GenericStoreResult> Store(string fileName, string fileContent)
        {

            string projectId = _GoogleCloudStorageConfiguration.ProjectId;
            string bucketName = _GoogleCloudStorageConfiguration.BucketName;

            //string credentialFile = System.Web.Hosting.HostingEnvironment.MapPath("~");
            string credentialFile = _environment.WebRootPath;
            credentialFile = credentialFile + _GoogleCloudStorageConfiguration.CredFile;
            //string bucketName = projectId + "-test-bucket";
            //string bucketName = projectId + "-" + bucket;

            // Instantiates a client.
            var credential = GoogleCredential.FromFile(credentialFile);
            StorageClient storageClient = await StorageClient.CreateAsync(credential);
            GenericStoreResult result = new GenericStoreResult();
            try
            {
                var bytes = Convert.FromBase64String(fileContent);
                var fileStream = new MemoryStream(bytes);
                storageClient.UploadObject(bucketName, fileName, null, fileStream);

                result.Result = 0;
                result.Volume = bucketName;
                result.FullPath = fileName;
                result.StorageType = GenericStorageTypeEnum.GCS.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"In Store: "+ex.Message);
                result.Result = 1;
                throw new Exception(ex.Message);
            }

            return result;
        }

        public override async Task<string> Restore(string volume, string fileFullPath)
        {
            try
            {
                string credentialFile = _environment.WebRootPath;
                credentialFile = credentialFile + _GoogleCloudStorageConfiguration.CredFile;
                var credential = GoogleCredential.FromFile(credentialFile);
                StorageClient storageClient = await StorageClient.CreateAsync(credential);

                using (var outputFile = new MemoryStream())
                {
                    storageClient.DownloadObject(volume, fileFullPath, outputFile);
                    return Convert.ToBase64String(outputFile.ToArray());
                }

                return "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "In Store: " + ex.Message);
                //result.Result = 1;
                throw new Exception(ex.Message);
            }
        }

        public override async void Delete(string volume, string fileFullPath)
        {
            try
            {
                string credentialFile = _environment.WebRootPath;
                credentialFile = credentialFile + _GoogleCloudStorageConfiguration.CredFile;
                var credential = GoogleCredential.FromFile(credentialFile);
                StorageClient storageClient = await StorageClient.CreateAsync(credential);

                storageClient.DeleteObject(volume, fileFullPath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "In Delete: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
