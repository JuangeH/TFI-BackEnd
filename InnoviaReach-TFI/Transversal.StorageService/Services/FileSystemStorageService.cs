using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Transversal.Helpers;
using Transversal.StorageService.Configurations;
using Transversal.StorageService.Contracts;

namespace Transversal.StorageService.Services
{
    public class FileSystemStorageService : GenericStorageService, IFileSystemStorageService
    {
        private readonly FileSystemStorageConfiguration _fileSystemStorageConfiguration;
        private readonly ILogger<FileSystemStorageService> _logger;

        public FileSystemStorageService(
            FileSystemStorageConfiguration FileSystemStorageConfiguration,
            ILogger<FileSystemStorageService> logger
            )
        {
            _fileSystemStorageConfiguration = FileSystemStorageConfiguration;
            _logger = logger;
        }

        public override async Task<GenericStoreResult> Store(string filename, string contentFile)
        {
            string fileFullPath = string.Empty;

            string volume = _fileSystemStorageConfiguration.CurrentVolume;
            string pathType = _fileSystemStorageConfiguration.CurrentPathType;

            if (pathType == "ABSOLUTE")
            {
                fileFullPath = _fileSystemStorageConfiguration.VolumeAbsolute;
            }
            else
            {
                fileFullPath = ServerInfoHelper.MapPath(_fileSystemStorageConfiguration.VolumeRelative);
            }

            fileFullPath = Path.Combine(fileFullPath, filename);

            GenericStoreResult result = new GenericStoreResult();
            try
            {
                await File.WriteAllBytesAsync(fileFullPath, Convert.FromBase64String(contentFile));

                result.Result = 0;
                result.Volume = volume;
                result.FullPath = fileFullPath;
                result.StorageType = "FSS";
            }
            catch (Exception ex)
            {
                result.Result = 1;
                _logger.LogError(ex, "In Store: " + ex.Message);
                throw new Exception(ex.Message);
            }

            return result;
        }

        public override async Task<string> Restore(string volume, string fileFullPath)
        {
            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(fileFullPath);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                _logger.LogError(ex, "In Restore: " + ex.Message);
                return null;
            }
        }

        public override void Delete(string volume, string fileFullPath)
        {
            try
            {
                File.Delete(fileFullPath);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "In Delete: " + ex.Message);
                throw ex;
            }

        }
    }
}
