using AutoMapper;
using Microsoft.Extensions.Logging;
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

    private readonly ISpaceShipService _shipService;

    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    #endregion

    #region constructor

    public GameStepService(
        ISpaceShipService shipService,
        IMapper mapper,
        ILogger<GameStepService> logger)
    {
        _shipService = shipService;

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

        var ship = _shipService.GetShip(id)
            ?? throw new NotFoundException($"Ship with id {id} not found.");

        ship.Step++;

        return _shipService.UpdateShip(id, ship);
    }
}
