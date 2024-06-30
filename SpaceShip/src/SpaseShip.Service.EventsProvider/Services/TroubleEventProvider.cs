using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SpaceShip.Services.Queue;

public class TroubleEventProvider : IHostedService
{
    private ILogger _logger;
    private IConnection _connection;
    private IModel _channel; 
    
    public TroubleEventProvider(ILogger<TroubleEventProvider> logger)
    {
        _logger = logger;
        var factory = new ConnectionFactory { 
                                    HostName = "localhost", 
                                    UserName = "space-ship", 
                                    Password = "Ss1234"};
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

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
            _channel.BasicConsume(queue: "troubles",
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