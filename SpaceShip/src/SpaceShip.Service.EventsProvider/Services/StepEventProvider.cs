using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Провайдер для сообщения нового игрового дня (шага) от игрового контроллера
/// </summary>
public class StepEventProvider : IHostedService
{
    private readonly ILogger _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _host;
    private readonly string _user;
    private readonly string _password;
    private readonly string _queue;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="logger">логер</param>
    /// <param name="configuration">конфигурация для доступа к переменным окружения</param>
    public StepEventProvider(ILogger<TroubleEventProvider> logger, IConfiguration configuration)
    {
        _logger = logger;
        _host = configuration["RABBITMQ_HOST"];
        _user = configuration["RABBITMQ_USER"];
        _password = configuration["RABBITMQ_PASSWORD"];
        _queue = configuration["RABBITMQ_STEP_QUEUE"];

        _logger.LogInformation("Trying to connect to RabbitMQ using AMPQ on host {_host}", _host);

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
            _logger.LogInformation("Succesfully connectedd to host {_host}", _host);
        }
        catch
        {
            _logger.LogError("Fail to connect RabbitMQ host {_host}", _host);
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("[GameStepProvider] Received new message: {message}", message);
        };

        try
        {
            _channel.BasicConsume(
                            queue: _queue,
                            autoAck: true,
                            consumer: consumer);
        }
        catch
        {
            _logger.LogError("[GameStepProvider] Error while try to consume new message");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[GameStepProvider] Provider finishing");
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}
