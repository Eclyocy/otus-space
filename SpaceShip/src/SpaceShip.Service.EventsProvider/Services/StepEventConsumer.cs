using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceShip.Notifications;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.EventsConsumer.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений нового игрового дня (шага) от игрового контроллера.
/// </summary>
public class StepEventConsumer : EventConsumer
{
    private readonly IServiceScopeFactory _scopeServiceFactory;
    private readonly INotificationsProvider _notificationsProvider;
    private readonly ILogger<StepEventConsumer> _logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public StepEventConsumer(
        ILogger<StepEventConsumer> logger,
        IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory,
        INotificationsProvider notificationsProvider)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_STEP_QUEUE"];
        ExchangeName = configuration["RABBITMQ_STEP_EXCHANGE"];
        ConsumerName = nameof(StepEventConsumer);
        _scopeServiceFactory = serviceScopeFactory;
        _notificationsProvider = notificationsProvider;
    }

    /// <inheritdoc/>
    protected override async Task HandleMessageAsync(string message, string routingKey = "")
    {
        StepMessageDTO? stepMessage;

        try
        {
            stepMessage = JsonConvert.DeserializeObject<StepMessageDTO>(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to deserialize the message: {message}", message);

            return;
        }

        if (stepMessage == null)
        {
            _logger.LogError("Step message is null.");

            return;
        }

        try
        {
            using IServiceScope scope = _scopeServiceFactory.CreateScope();
            IShipService shipService = scope.ServiceProvider.GetRequiredService<IShipService>();

            ShipDTO ship = shipService.ProcessNewDay(stepMessage.ShipId);
            await _notificationsProvider.SendAsync(stepMessage.ShipId, ship);
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Failed to process new day for ship with id {id}", stepMessage.ShipId);
        }
    }
}
