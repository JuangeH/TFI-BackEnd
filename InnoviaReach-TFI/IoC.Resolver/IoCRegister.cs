using Core.Business.Services;
using Core.Contracts.Services;
using IoC.Resolver.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Transversal.EmailService.Configurations;
using Transversal.EmailService.Contracts;
using Transversal.EmailService.Factory;
using Transversal.EmailService.Services;
using Transversal.Extensions;
using Transversal.StorageService.Configurations;
using Transversal.StorageService.Contracts;
using Transversal.StorageService.Factory;
using Transversal.StorageService.Services;

namespace IoC.Resolver
{
    public static class IoCRegister
    {
        public static IServiceCollection ConfigureIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDataLayer(configuration);
            services.RegisterUnitOfWork();
            services.RegisterBusinessLayer();
            services.RegisterEmails(configuration);
            services.RegisterStorages(configuration);
            return services;
        }

        private static IServiceCollection RegisterEmails(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService>();

            //services.AddTransient(typeof(IGenericEmailService), typeof(GenericEmailService));
            services.AddTransient<IEmailSendGridService, EmailSendgridService>();
            services.AddTransient<IEmailSmtpService, EmailSmtpService>();
            services.AddTransient<IGenericEmailFactory, GenericEmailFactory>();

            services.AddConfig<EmailConfiguration>(configuration, nameof(EmailConfiguration));
            services.AddConfig<EmailSendGridConfiguration>(configuration, nameof(EmailConfiguration) + "." + nameof(EmailSendGridConfiguration));
            services.AddConfig<EmailSMTPConfiguration>(configuration, nameof(EmailConfiguration) + "." + nameof(EmailSMTPConfiguration));

            return services;
        }

        private static IServiceCollection RegisterStorages(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IGenericStorageService, GenericStorageService>();
            services.AddTransient<IAzureBlobStorageService, AzureBlobStorageService>();
            services.AddTransient<IGoogleCloudStorageService, GoogleCloudStorageService>();
            services.AddTransient<IFileSystemStorageService, FileSystemStorageService>();
            services.AddTransient<IGenericStorageServiceFactory, GenericStorageServiceFactory>();

            services.AddConfig<GenericStorageConfiguration>(configuration, nameof(GenericStorageConfiguration));
            services.AddConfig<FileSystemStorageConfiguration>(configuration, nameof(GenericStorageConfiguration) + "." + nameof(FileSystemStorageConfiguration));
            services.AddConfig<GoogleCloudStorageConfiguration>(configuration, nameof(GenericStorageConfiguration) + "." + nameof(GoogleCloudStorageConfiguration));
            services.AddConfig<AzureBlobStorageConfiguration>(configuration, nameof(GenericStorageConfiguration) + "." + nameof(AzureBlobStorageConfiguration));

            return services;
        }


        //Mas adelante, la idea es mudar todos los configurations a un nuevo proyecto en donde solo se guardan las configs y levantarlos con reflexión.
    }
}
