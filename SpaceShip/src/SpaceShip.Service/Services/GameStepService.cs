using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;

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
    private readonly IMapper _mapper;

    #endregion

    #region constructor

    public GameStepService(
        ISpaceshipRepository shipRepository,
        IMapper mapper,
        ILogger<GameStepService> logger)
    {
        _shipRepository = shipRepository;
        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    /// <summary>
    /// Применить новый игровой день (новый шаг)
    /// </summary>
    /// <param name="id">ID корабля</param>
    public async Task<SpaceShipDTO> ProcessNewDayAsync(Guid id)
    {
        _logger.LogInformation("Process new day for ship with id {id}", id);

        var ship = _shipRepository.Get(id);

        if (ship == null)
        {
            throw new NotFoundException(string.Format("Ship with id {0} not found in ships service", id));
        }

        ship.Step++;
        _logger.LogInformation("Update ship {id} in repository. Set step {step} ", id, ship.Step);
        _shipRepository.Update(ship);

        return _mapper.Map<SpaceShipDTO>(ship);
    }
}
