using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SpaceShip.Service.EventsConsumer.Contracts;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Queue;

/// <summary>
/// Консьюмер сообщений нового игрового дня (шага) от игрового контроллера.
/// </summary>
public class StepEventConsumer : EventConsumer
{
    private readonly IServiceScopeFactory _scopeServiceFactory;
    public StepEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_STEP_QUEUE"];
        ConsumerName = nameof(StepEventConsumer);
        _scopeServiceFactory = serviceScopeFactory;
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

            await dayServiceScoped.ProcessNewDayAsync(stepMessage.ShipId);
        }
    }
}
