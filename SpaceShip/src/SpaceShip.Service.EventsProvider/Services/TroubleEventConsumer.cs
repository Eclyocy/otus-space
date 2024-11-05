using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceShip.Notifications;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.EventsConsumer.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Models;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений от генератора событий.
/// </summary>
public class TroubleEventConsumer : EventConsumer
{
    private readonly IServiceScopeFactory _scopeServiceFactory;
    private readonly INotificationsProvider _notificationsProvider;
    private readonly IMapper _mapper;
    private readonly ILogger<TroubleEventConsumer> _logger;

    /// <summary>
    /// Constructor.
    /// </summary>
    public TroubleEventConsumer(
        IServiceScopeFactory serviceScopeFactory,
        INotificationsProvider notificationsProvider,
        IMapper mapper,
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_TROUBLES_QUEUE"];
        ExchangeName = configuration["RABBITMQ_TROUBLES_EXCHANGE"];
        ConsumerName = nameof(TroubleEventConsumer);
        _scopeServiceFactory = serviceScopeFactory;
        _notificationsProvider = notificationsProvider;
        _mapper = mapper;
        _logger = logger;
    }

    protected override async Task HandleMessageAsync(string message, string routingKey)
    {
        TroubleMessageDTO? troubleMessage;

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

        Trouble trouble = _mapper.Map<Trouble>(troubleMessage);

        try
        {
            using IServiceScope scope = _scopeServiceFactory.CreateScope();
            IShipService shipService = scope.ServiceProvider.GetRequiredService<IShipService>();

            ShipDTO ship = shipService.ApplyFailure(trouble);
            await _notificationsProvider.SendAsync(trouble.ShipId, ship);
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Failed to process trouble message for ship with id {id}", trouble.ShipId);
        }
    }
}
