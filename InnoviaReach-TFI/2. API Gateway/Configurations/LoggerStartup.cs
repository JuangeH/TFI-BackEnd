using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Api.Configurations
{
    public static class LoggerStartup
    {
        [System.Obsolete]
        public static IServiceCollection ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var loggerConfig = new LoggerConfiguration()
                .Filter.ByExcluding(logEvent =>
                {
                    bool hasContext = logEvent.Properties.ContainsKey("SourceContext");

                    if (hasContext)
                    {
                        var sourceContext = (Serilog.Events.ScalarValue)logEvent.Properties["SourceContext"];
                        return sourceContext.Value.ToString() == "Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware";
                    }

                    return false;
                })
                .ReadFrom.Configuration(configuration, sectionName: "Serilog")
                .WriteTo.File("Log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("SqlConnection"),
                    tableName: configuration.GetSection("Serilog").GetSection("TableName").Value,
                    appConfiguration: configuration,
                    autoCreateSqlTable: true,
                    columnOptionsSection: configuration.GetSection("Serilog").GetSection("ColumnOptions"),
                    schemaName: configuration.GetSection("Serilog").GetSection("SchemaName").Value);
                //.WriteTo.Email(
                //    toEmail: configuration.GetSection("Serilog").GetSection("ToEmail").Value,
                //    fromEmail: configuration.GetSection("Serilog").GetSection("FromEmail").Value,
                //    mailSubject: configuration.GetSection("Serilog").GetSection("MailSubject").Value,
                //    mailServer: configuration.GetSection("Serilog").GetSection("MailServer").Value,
                //    new System.Net.NetworkCredential(
                //        userName: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("UserName").Value,
                //        password: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("Password").Value,
                //        domain: configuration.GetSection("Serilog").GetSection("NetworkCredentials").GetSection("Domain").Value));

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration.GetSection("Logging"))
                    .AddSerilog(loggerConfig.CreateLogger());
            });

            return services;
        }

        
    }
}
