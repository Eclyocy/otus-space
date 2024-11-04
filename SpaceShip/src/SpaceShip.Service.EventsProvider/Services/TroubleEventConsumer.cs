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
/// Консьюмер сообщений от генератора событий.
/// </summary>
public class TroubleEventConsumer : EventConsumer
{
    private readonly IServiceScopeFactory _scopeServiceFactory;
    private readonly INotificationsProvider _notificationsProvider;
    private readonly ILogger<TroubleEventConsumer> _logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public TroubleEventConsumer(
        IServiceScopeFactory serviceScopeFactory,
        INotificationsProvider notificationsProvider,
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_TROUBLES_QUEUE"];
        ExchangeName = configuration["RABBITMQ_TROUBLES_EXCHANGE"];
        ConsumerName = nameof(TroubleEventConsumer);
        _scopeServiceFactory = serviceScopeFactory;
        _notificationsProvider = notificationsProvider;
        _logger = logger;
    }

    protected override async Task HandleMessageAsync(string message, string routingKey)
    {
        TroubleMessageDTO? troubleMessage;

        if (!Guid.TryParse(routingKey, out Guid shipId))
        {
            _logger.LogError("Failed to parse message routing key [{routingKey}] to ship identifier", routingKey);
            return;
        }

        try
        {
            troubleMessage = JsonConvert.DeserializeObject<TroubleMessageDTO>(message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to deserialize the message: {message}", message);
            return;
        }

        if (troubleMessage == null)
        {
            _logger.LogError("Message is null.");
            return;
        }

        try
        {
            using IServiceScope scope = _scopeServiceFactory.CreateScope();
            IShipService shipService = scope.ServiceProvider.GetRequiredService<IShipService>();

            ShipDTO ship = shipService.ApplyFailure(shipId, troubleMessage.EventLevel);
            await _notificationsProvider.SendAsync(shipId, ship);
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Failed to process trouble message for ship with id {id}", shipId);
        }
    }
}
