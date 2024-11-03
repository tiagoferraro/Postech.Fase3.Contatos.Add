using System.Text;
using Microsoft.Extensions.Configuration;
using Polly;
using Postech.Fase3.Contatos.Add.Infra.CrossCuting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Postech.Fase3.Contatos.Add.Infra.Ioc.Messaging;

public class RabbitMqConsumer
{
    private readonly IMessageProcessor _messageProcessor;
    private readonly ConnectionFactory _connectionFactory;
    private readonly string? _filaConsummer;
    
    public RabbitMqConsumer(IMessageProcessor messageProcessor, IConfiguration configuration)
    {
        _messageProcessor = messageProcessor;
        var rabbitMqConfig = configuration.GetSection("RabbitMQ");
        _connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"]
        };
        _filaConsummer = rabbitMqConfig["QueueName"];
    }
    public void StartListening()
    {
        
        var retryPolicy = Policy
            .Handle<BrokerUnreachableException>() // Tenta novamente se o RabbitMQ não estiver acessível
            .Or<Exception>()                      // Ou se houver outra exceção
            .WaitAndRetry(
                retryCount: 5,                    // Número de tentativas
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(20 * attempt), // Intervalo entre tentativas
                onRetry: (exception, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Tentativa {retryAttempt} falhou. Tentando novamente em {timespan.Seconds} segundos...");
                }
            );
        
        retryPolicy.Execute(() =>
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _filaConsummer,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Processa a mensagem na camada de Aplicação
                await _messageProcessor.ProcessMessageAsync(message);
            };

            channel.BasicConsume(queue: "task_queue", autoAck: true, consumer: consumer);

            Console.WriteLine("Listening for messages on 'task_queue'...");
            Console.ReadLine(); // Mantém a aplicação escutando
        });
    }
}