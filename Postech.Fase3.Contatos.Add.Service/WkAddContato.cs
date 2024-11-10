using Postech.Fase3.Contatos.Add.Infra.Ioc.Messaging;

namespace Postech.Fase3.Contatos.Add.Service;

public class WkAddContato(
    ILogger<WkAddContato> _logger,
    RabbitMqConsumer _rabbitMqConsumer
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        await _rabbitMqConsumer.StartListeningAsync();
    }
}