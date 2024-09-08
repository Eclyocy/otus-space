using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Notifications;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для обработки события нового игрового дня (шага игры).
/// Регистрируется как ScopedService для работы с обработчиками сообщений из очереди RabbitMQ.
/// </summary>
public class GameStepService : IGameStepService
{
    #region private fields

    private readonly ILogger _logger;
    private readonly ISpaceshipRepository _shipRepository;

    // private readonly INotificationsHub _notificationsHub;
    private readonly IHubContext<NotificationsHub> _hubContext;

    #endregion

    #region constructor

    public GameStepService(
        ISpaceshipRepository shipRepository,
        ILogger<SpaceShipService> logger,
        IHubContext<NotificationsHub> hubContext)
    {
        _shipRepository = shipRepository;
        _logger = logger;
        _hubContext = hubContext;
    }

    #endregion

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public async Task ProcessNewDayAsync(Guid id)
    {
        _logger.LogInformation("Process new day for ship with id {id}", id);

        var ship = _shipRepository.Get(id);

        if (ship != null)
        {
            ship.Step++;

            _logger.LogInformation("Update ship {id} in repository. Set step {step} ", id, ship.Step);
            _shipRepository.Update(ship);

            _logger.LogInformation("Try to notify about ship {id} changes", id);
            await _hubContext.Clients.All.SendAsync(nameof(GameStepService), ship);
        }
    }
}
