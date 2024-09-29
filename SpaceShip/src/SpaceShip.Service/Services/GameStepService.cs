using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Model.State;
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

    private readonly IShipService _shipService;

    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    #endregion

    #region constructor

    public GameStepService(
        IShipService shipService,
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

        var updateResources = ship.Resources;

        foreach (var i in updateResources)
        {
            foreach (var j in i.Resources)
            {
                if (j.Amount >= 10)
                {
                    j.Amount = j.Amount - 10;
                }

                if (j.Amount == 0)
                {
                    ship.State = SpaceshipState.Sleep;
                }
            }
        }

        ship.Step++;

        return _shipService.UpdateShip(id, ship);
    }
}
