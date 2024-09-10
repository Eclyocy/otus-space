using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений от генератора событий.
/// </summary>
public class TroubleEventConsumer : EventConsumer
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TroubleEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_TROUBLES_QUEUE"];
        ConsumerName = nameof(TroubleEventConsumer);
    }

    protected override Task HandleMessageAsync(string message)
    {
        throw new NotImplementedException();
    }
}
