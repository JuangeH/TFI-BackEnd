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
                    // Se ejecuta cada 5 minutos
                    if (DateTime.Now.Minute % 30 == 0 && _lastExecution.Minute != DateTime.Now.Minute)
                    {
                        _lastExecution = DateTime.Now;
                        _logger.LogInformation($"Updating adquisicion table at {DateTime.Now}");
                        using var scope = _serviceProvider.CreateScope();

                        var adquisicionService = scope.ServiceProvider.GetRequiredService<IAdquisicionService>();
                        var steamAccountService = scope.ServiceProvider.GetRequiredService<ISteamAccountService>();

                        foreach (var steamAccount in await steamAccountService.GetAllAsync())
                        {
                            try
                            {
                                var steamInfoRequest = new Request.SteamInfoRequest();
                                steamInfoRequest.SteamAPIKey = String.IsNullOrEmpty(steamAccount.ApiKey) ? "DB87EDFDEF1A6EC905BD4F1F51B2377A":steamAccount.ApiKey;
                                steamInfoRequest.SteamID = steamAccount.steamid;
                                //await adquisicionService.ActualizarRegistrarAdquisiciones(steamInfoRequest, steamAccount.User_ID);
                                await adquisicionService.ActualizarJugadoReciente(steamInfoRequest, steamAccount.User_ID);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,$"Error in user:{steamAccount.User_ID}");
                                continue;                            
                            }
                        }
                    }

                    await Task.Delay(50);
                }
            }
        }
    }
}