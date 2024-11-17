using Postech.Fase3.Contatos.Add.Infra.Messaging;

namespace Postech.Fase3.Contatos.Add.Service;

public class WkAddContato(
    ILogger<WkAddContato> _logger,
    RabbitMqConsumer _rabbitMqConsumer
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        _rabbitMqConsumer.StartListeningAsync();
        return Task.CompletedTask;
    }
}