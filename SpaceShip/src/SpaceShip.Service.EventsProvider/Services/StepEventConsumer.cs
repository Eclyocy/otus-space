using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений нового игрового дня (шага) от игрового контроллера.
/// </summary>
public class StepEventConsumer : EventConsumer
{
    public StepEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_STEP_QUEUE"];
        ConsumerName = nameof(StepEventConsumer);
    }
}
