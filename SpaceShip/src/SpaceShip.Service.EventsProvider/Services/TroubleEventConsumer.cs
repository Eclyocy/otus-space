using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений от генератора событий.
/// </summary>
public class TroubleEventConsumer : EventConsumer
{
    public TroubleEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_TROUBLES_QUEUE"];
        ExchangeName = configuration["RABBITMQ_TROUBLES_EXCHANGE"];
        ConsumerName = nameof(TroubleEventConsumer);
    }

    protected override void HandleMessage(string message)
    {
        throw new NotImplementedException();
    }
}
