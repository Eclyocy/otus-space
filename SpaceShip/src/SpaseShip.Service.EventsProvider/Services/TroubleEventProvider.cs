using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace SpaceShip.Services.Queue;

public class TroubleEventProvider : IHostedService
{
    private ILogger _logger;
    private IConnection _connection;
    private IModel _channel; 

    private readonly string _host;
    private readonly string _user;
    private readonly string _password;
    private readonly string _queue;
    public TroubleEventProvider(ILogger<TroubleEventProvider> logger, IConfiguration configuration)
    {
        _logger = logger;
        _host = configuration["RABBITMQ_HOST"];
        _user = configuration["RABBITMQ_USER"];
        _password = configuration["RABBITMQ_PASSWORD"];
        _queue = configuration["RABBITMQ_TROUBLES_QUEUE"];

        _logger.LogInformation("Trying to connect to RabbitMQ using AMPQ on host {_host}",_host);

        var factory = new ConnectionFactory { 
                                    HostName = _host, 
                                    UserName = _user, 
                                    Password = _password};
        try 
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        catch 
        {
            _logger.LogError("Fail to connect RabbitMQ host {_host}",_host);
        }
        
        _logger.LogInformation("Succesfully connected to host {_host}",_host);

    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("[TroubleProvider] Received new message: {message}",message);
        };
        try
        {
            _channel.BasicConsume(queue: _queue,
                            autoAck: true,
                            consumer: consumer);
        }
        catch
        {
            _logger.LogError("[TroubleProvider] Error while trying to consume new message");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("[TroubleProvider] Provider finishing");
        _channel.Close();
        _connection.Close();
        return Task.CompletedTask;
    }
}