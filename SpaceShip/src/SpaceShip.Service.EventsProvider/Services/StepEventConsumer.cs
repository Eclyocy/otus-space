using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceShip.Notifications;
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

    public StepEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory,
        INotificationsProvider notificationsProvider)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_STEP_QUEUE"];
        ConsumerName = nameof(StepEventConsumer);
        _scopeServiceFactory = serviceScopeFactory;
        _notificationsProvider = notificationsProvider;
    }

    /// <inheritdoc/>
    protected override async Task HandleMessageAsync(string message)
    {
        StepMessageDTO? stepMessage = JsonConvert.DeserializeObject<StepMessageDTO>(message);

        if (stepMessage == null)
        {
            throw new Exception("Unable to parse step message.");
        }

        using (IServiceScope scope = _scopeServiceFactory.CreateScope())
        {
            IGameStepService dayServiceScoped =
                scope.ServiceProvider.GetRequiredService<IGameStepService>();

            var ship = await dayServiceScoped.ProcessNewDayAsync(stepMessage.ShipId);
            await _notificationsProvider.SendAsync(stepMessage.ShipId, ship);
        }
    }
}
