using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Interfaces;

namespace SpaceShip.Service.Implementation;

/// <summary>
/// Сервис для обработки события новго игоровго дня (шага игры).
/// Регистрируется как ScopedService для работы с обработчиками сообщений из очереди RabbitMQ.
/// </summary>
public class GameStepService : IGameStepService
{
    #region private fields

    private readonly ILogger _logger;
    private readonly ISpaceshipRepository _shipRepository;

    #endregion

    #region constructor

    public GameStepService(
        ISpaceshipRepository shipRepository,
        ILogger<SpaceShipService> logger)
    {
        _shipRepository = shipRepository;
        _logger = logger;
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
        }
    }
}
