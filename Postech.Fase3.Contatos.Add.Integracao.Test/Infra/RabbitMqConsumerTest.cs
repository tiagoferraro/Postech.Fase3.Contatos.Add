
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using Postech.Fase3.Contatos.Add.Application.DTO;
using Postech.Fase3.Contatos.Add.Infra.Messaging;
using RabbitMQ.Client;
using Serilog;
using Testcontainers.RabbitMq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Postech.Fase3.Contatos.Add.Integracao.Test.Infra;

public class RabbitMqConsumerTest
{
    [Fact]
    public async Task RabbitMqConsumer_QuandoMensagemValida_DeveRetornarSucesso()
    {
        // Arrange
        var serviceProviderMock = new Mock<IServiceProvider>();
        var logg = new Mock<ILogger>();

        var rabbitMqSettings = new Dictionary<string, string>
        {
            { "RabbitMq:HostName", "localhost" },
            { "RabbitMq:QueueName", "Fase3.Contatos.AddtoDataBase" },
            { "RabbitMq:Exchange", "Fase3.Contatos.Add" },
            { "RabbitMq:UserName", "user" },
            { "RabbitMq:Password", "password" },
            { "RabbitMq:Port", "15673" }
        };


        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(rabbitMqSettings!)
            .Build();

        
        var rabbitMqConfig = configuration.GetSection("RabbitMq");

        var rabbitMqContainer = new RabbitMqBuilder()
             .WithImage("rabbitmq:3.11")
             .WithUsername(rabbitMqConfig["UserName"])
             .WithPassword(rabbitMqConfig["Password"])
             .WithPortBinding(rabbitMqConfig["Port"],"5672" )
             .Build();
        await rabbitMqContainer.StartAsync();

        var connectionFactory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"]!,
            UserName = rabbitMqConfig["UserName"]!,
            Password = rabbitMqConfig["Password"]!,
            Port = Convert.ToInt32(rabbitMqConfig["Port"]),
        };
        if (connectionFactory == null) throw new ArgumentNullException(nameof(connectionFactory));

        await using var connection = await connectionFactory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();


        await channel.QueueDeclareAsync(queue: rabbitMqConfig["QueueName"]!,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        await channel.ExchangeDeclareAsync(rabbitMqConfig["Exchange"]!, ExchangeType.Fanout, true, false);
        await channel.QueueBindAsync(rabbitMqConfig["QueueName"]!, rabbitMqConfig["Exchange"]!, "");


        var contatoDto = new ContatoDto(Guid.NewGuid(), "Nome teste", "963333243", "email@email.com", true, 11, DateTime.Now);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(contatoDto));

        // Publica a mensagem na fila
        await channel.BasicPublishAsync(
            exchange: rabbitMqConfig["Exchange"]!,
            routingKey: "",
            body: body);

        var rabbitMqConsumer = new RabbitMqConsumer(configuration, serviceProviderMock.Object, logg.Object);

        // Act
        rabbitMqConsumer.StartListeningAsync();
    }
}

