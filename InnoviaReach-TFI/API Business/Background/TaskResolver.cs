using Core.Business.Services;
using Core.Contracts.Services;
using Microsoft.IdentityModel.Tokens;

namespace API_Business.Background
{

    namespace Api.Background
    {
        public class TasksResolver : BackgroundService
        {
            private readonly ILogger<TasksResolver> _logger;
            private readonly IServiceProvider _serviceProvider;

            /// <summary>
            /// Marca de tiempo utilizada como condicion en la ejecucion de varios mÃ©todos
            /// </summary>
            private DateTime _lastExecution;
            /// <summary>
            /// Marca de tiempo utilizada como condicion en la ejecucion de FinalizeMeetings
            /// </summary>
            private DateTime _lastFinalizeMeetingsExecution;

            public TasksResolver(ILogger<TasksResolver> logger, IServiceProvider serviceProvider)
            {
                _logger = logger;
                _serviceProvider = serviceProvider;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                _logger.LogInformation("BackgroundService Running");

                while (!stoppingToken.IsCancellationRequested)
                {
                    if (DateTime.Now.Hour == 2 && DateTime.Now.Minute == 0 && _lastExecution.Date != DateTime.Now.Date)
                    {
                        _lastExecution = DateTime.Now;
                        _logger.LogInformation($"Starting daily tasks at {DateTime.Now}");

                        using var scope = _serviceProvider.CreateScope();
                        var recomendacionService = scope.ServiceProvider.GetRequiredService<IRecomendacionService>();

                        try
                        {
                            // Crear clusters de usuarios
                            await recomendacionService.CrearClustersUsuarios();
                            _logger.LogInformation("Clusters de usuarios creados exitosamente");

                            await recomendacionService.GenerarRecomendacionesColaborativas();
                            _logger.LogInformation($"Recomendaciones generadas para el usuario");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error ejecutando las tareas diarias");
                        }
                    }

                    await Task.Delay(50);
                }
            }
        }
    }
}