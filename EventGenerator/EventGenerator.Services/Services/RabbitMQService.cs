using System.Text;
using System.Text.Json;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EventGenerator.Services.Services;

public class RabbitMQService : IHostedService
{
    #region private fields

    private readonly ILogger<RabbitMQService> _logger;
    private readonly IServiceScopeFactory _scopeServiceFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _host;
    private readonly string _user;
    private readonly string _password;
    private readonly string _newDayQueue;
    private readonly string _consumerName;
    private readonly string _eventsExchange;
    private readonly string _newDayExchange;

    #endregion

    #region constructor

    public RabbitMQService(ILogger<RabbitMQService> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _scopeServiceFactory = serviceScopeFactory;

        _host = configuration["RABBITMQ_HOST"] ?? throw new Exception("RabbitMQ hostname does not set");
        _user = configuration["RABBITMQ_USER"] ?? throw new Exception("RabbitMQ username does not set");
        _password = configuration["RABBITMQ_PASSWORD"] ?? throw new Exception("RabbitMQ user password does not set");

        _newDayExchange = configuration["RABBITMQ_NEW_DAY_EXCHANGE"] ?? throw new Exception("RabbitMQ exchange name for new day messages does not set");
        _newDayQueue = configuration["RABBITMQ_NEW_DAY_QUEUE"] ?? "new-days-generator-queue";

        _eventsExchange = configuration["RABBITMQ_EVENTS_EXCHANGE"] ?? "generated-events";

        _consumerName = nameof(RabbitMQService);

        _logger.LogInformation("Trying to connect to RabbitMQ using AMQP on host {_host}", _host);
        var factory = new ConnectionFactory
        {
            HostName = _host,
            UserName = _user,
            Password = _password
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _logger.LogInformation("Successfully connected to host {_host}", _host);
        }
        catch
        {
            _logger.LogError("Failed to connect RabbitMQ host {_host}", _host);
            throw;
        }

        _channel.ExchangeDeclare(
                    exchange: _eventsExchange,
                    type: "topic",
                    durable: true,
                    autoDelete: false);

        CreateQueue(_newDayQueue);
        BindQueue(_newDayQueue, _newDayExchange);
    }

    #endregion

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation("{consumer} received new message: {message}", _consumerName, message);

            await HandleMessageAsync(message, cancellationToken);
        };

        try
        {
            _channel.BasicConsume(
                queue: _newDayQueue,
                autoAck: true,
                consumer: consumer);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{consumer} failed to consume new message", _consumerName);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{consumer} stopping", _consumerName);

        _channel.Close();
        _connection.Close();

        return Task.CompletedTask;
    }

    private async Task HandleMessageAsync(string message, CancellationToken cancellationToken)
    {
        NewDayMessageDto? stepMessage = JsonSerializer.Deserialize<NewDayMessageDto>(message);

        if (stepMessage == null)
        {
            throw new Exception("Unable to parse new day message.");
        }

        Guid generatorId = stepMessage.GeneratorId;
        Guid shipId = stepMessage.ShipId;

        EventDto? problem = null;

        using (IServiceScope scope = _scopeServiceFactory.CreateScope())
        {
            IGeneratorService generatorServiceScoped =
                scope.ServiceProvider.GetRequiredService<IGeneratorService>();

            _logger.LogInformation("{consumer} add coins to event generator with id \"{id}\"", _consumerName, generatorId);
            generatorServiceScoped.AddTroubleCoin(generatorId);

            _logger.LogInformation("{consumer} ask event generator with id \"{id}\" for new event", _consumerName, generatorId);
            problem = generatorServiceScoped.GenerateEvent(generatorId);
        }

        if (problem == null)
        {
            _logger.LogInformation("No new event from generator with id \"{id}\"", generatorId);
            return;
        }

        var problemMessage = new EventMessageDto
        {
            ShipId = shipId,
            EventId = problem.EventId,
            EventLevel = problem.EventLevel
        };
        SendMessage(JsonSerializer.Serialize(problemMessage), shipId.ToString());
    }

    private void CreateQueue(string queueName)
    {
        try
        {
            _channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot create queue {queue}.", queueName);
            throw;
        }
    }

    private void BindQueue(string queueName, string exchangeName, string? routingKey = "")
    {
        try
        {
            _channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: routingKey);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot bind queue {queue} to exchange {exchange}", queueName, exchangeName);
            throw;
        }
    }

    private void SendMessage(string message, string routingKey = "")
    {
        var body = Encoding.UTF8.GetBytes(message);

        IBasicProperties basicProperties = _channel.CreateBasicProperties();
        basicProperties.Persistent = true;

        _channel.BasicPublish(
            exchange: _eventsExchange,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: basicProperties,
            body: body);
    }
}
