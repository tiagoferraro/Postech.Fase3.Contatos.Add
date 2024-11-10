using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Postech.Fase3.Contatos.Add.Infra.Ioc.Messaging;

public class RabbitMqConsumer
{

    private readonly ConnectionFactory _connectionFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _filaConsummer;
    private readonly string _exchange;

    public RabbitMqConsumer( IConfiguration configuration, IServiceProvider serviceProvider)
    {
      
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        _connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"]
        };
        _serviceProvider = serviceProvider;
        _filaConsummer = rabbitMqConfig["QueueName"] ?? throw new ArgumentNullException(nameof(configuration));
        _exchange = rabbitMqConfig["Exchange"] ?? throw new ArgumentNullException(nameof(configuration)); 
    }
    public Task StartListeningAsync()
    {
        
        var retryPolicy = Policy
            .Handle<BrokerUnreachableException>() // Tenta novamente se o RabbitMQ não estiver acessível
            .Or<Exception>()                      // Ou se houver outra exceção
            .WaitAndRetry(
                retryCount: 5,                    // Número de tentativas
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(20 * attempt), // Intervalo entre tentativas
                onRetry: (exception, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine("Erro RabbitMQ: " + exception.Message);
                    Console.WriteLine($"Tentativa {retryAttempt} falhou. Tentando novamente em {timespan.Seconds} segundos...");
                }
            );
        
        retryPolicy.Execute(async () =>
        {
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: _filaConsummer,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            await channel.ExchangeDeclareAsync(_exchange,ExchangeType.Fanout,true,false);
            await channel.QueueBindAsync(_filaConsummer, _exchange, "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageProcessor = scope.ServiceProvider.GetRequiredService<IMessageProcessor>();
                    
                    await messageProcessor.ProcessMessageAsync(message);
                }

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            await channel.BasicConsumeAsync(queue: _filaConsummer, autoAck: false, consumer: consumer);

            Console.WriteLine($"Escutando a fila {_filaConsummer}...");
            Console.ReadLine(); // Mantém a aplicação escutando
        });

        return Task.CompletedTask;
    }
}