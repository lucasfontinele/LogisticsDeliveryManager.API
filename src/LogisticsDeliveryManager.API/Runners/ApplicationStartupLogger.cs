using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace LogisticsDeliveryManager.API.Runners;

public class ApplicationStartupLogger : BackgroundService
{
    private readonly ILogger<ApplicationStartupLogger> _logger;
    private readonly IHostEnvironment _env;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHostApplicationLifetime _lifetime;

    public ApplicationStartupLogger(
        ILogger<ApplicationStartupLogger> logger,
        IHostEnvironment env,
        IServiceProvider serviceProvider,
        IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _env = env;
        _serviceProvider = serviceProvider;
        _lifetime = lifetime;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Usamos o ApplicationStarted.Register para garantir que isso rode APÓS o servidor subir
        // Isso é o equivalente exato ao ApplicationRunner do Spring Boot
        _lifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                var appName = "Logistics Delivery Manager API";
                var isDev = _env.IsDevelopment();

                if (isDev)
                {
                    // Obtém dinamicamente a URL exata em que o projeto subiu (sem precisar ler config manualmente)
                    var server = _serviceProvider.GetRequiredService<IServer>();
                    var addresses = server.Features.Get<IServerAddressesFeature>()?.Addresses;
                    
                    var address = addresses?.FirstOrDefault(a => a.StartsWith("https://")) 
                               ?? addresses?.FirstOrDefault(a => a.StartsWith("http://")) 
                               ?? "https://localhost:5001";

                    _logger.LogInformation(
                        "\n----------------------------------------------------------\n\t" +
                        "Application '{AppName}' is running in DEV mode! Access URLs:\n\t" +
                        "Local: \t\t{Address}\n\t" +
                        "Swagger: \t{Address}/swagger/index.html\n" +
                        "----------------------------------------------------------",
                        appName,
                        address,
                        address
                    );
                }
                else
                {
                    _logger.LogInformation(
                        "\n----------------------------------------------------------\n\t" +
                        "Application '{AppName}' is running!\n" +
                        "----------------------------------------------------------",
                        appName
                    );
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar printar os logs de inicialização.");
            }
        });

        return Task.CompletedTask;
    }
}
