using Microsoft.Extensions.Configuration;
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
    private readonly IShipService _shipService;

    public StepEventConsumer(
        ILogger<TroubleEventConsumer> logger,
        IConfiguration configuration,
        IShipService shipService)
        : base(logger, configuration)
    {
        QueueName = configuration["RABBITMQ_STEP_QUEUE"];
        ConsumerName = nameof(StepEventConsumer);

        _shipService = shipService;
    }

    protected override void HandleMessage(string message)
    {
        StepMessageDTO? stepMessage = JsonConvert.DeserializeObject<StepMessageDTO>(message);

        if (stepMessage == null)
        {
            throw new Exception("Unable to parse step message.");
        }

        Console.WriteLine(stepMessage);
        Console.WriteLine(stepMessage.ShipId);

        _shipService.ProcessNewDay(stepMessage.ShipId);
    }
}
