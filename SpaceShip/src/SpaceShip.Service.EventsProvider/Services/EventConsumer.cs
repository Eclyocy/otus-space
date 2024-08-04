using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Базовый обработчик сообщений из брокера.
/// </summary>
public abstract class EventConsumer : IHostedService
{
    private readonly ILogger _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    private readonly string _host;
    private readonly string _user;
    private readonly string _password;

    /// <summary>
    /// Конструктор.
    /// </summary>
    /// <param name="logger">логгер</param>
    /// <param name="configuration">конфигурация для доступа к переменным окружения</param>
    protected EventConsumer(
        ILogger logger,
        IConfiguration configuration)
    {
        _logger = logger;

        _host = configuration["RABBITMQ_HOST"];
        _user = configuration["RABBITMQ_USER"];
        _password = configuration["RABBITMQ_PASSWORD"];

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
        }
    }

    #region public properties

    /// <summary>
    /// Имя exchange в который приходят сообщения.
    /// </summary>
    public string ExchangeName { get; protected set; }

    /// <summary>
    /// Имя очереди, сообщения из которой принимаем.
    /// </summary>
    public string QueueName { get; protected set; }

    /// <summary>
    /// Название обработчика.
    /// </summary>
    public string ConsumerName { get; protected set; }

    #endregion

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        CreateQueue(QueueName);
        BindQueue(QueueName, ExchangeName);

        _logger.LogInformation("Queue {queueName} connected to exchange {exchangeName}. Ready to consume new messages", QueueName, ExchangeName);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation("{consumer} received new message: {message}", ConsumerName, message);

            HandleMessage(message);
        };

        try
        {
            _channel.BasicConsume(
                queue: QueueName,
                autoAck: true,
                consumer: consumer);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{consumer} failed to consume new message", ConsumerName);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{consumer} stopping", ConsumerName);

        _channel.Close();
        _connection.Close();

        return Task.CompletedTask;
    }

    protected abstract void HandleMessage(string message);

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
        }
    }

    private void BindQueue(string queueName, string exchangeName)
    {
        try
        {
            _channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: string.Empty);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Cannot bind queue {queue} to exchange {exchange}", queueName, exchangeName);
        }
    }
}
