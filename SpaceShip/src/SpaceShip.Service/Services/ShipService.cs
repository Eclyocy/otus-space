using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;

namespace SpaceShip.Service.Services;

/// <summary>
/// Сервис для работы с сущностью "Корабль".
/// </summary>
public class ShipService : IShipService
{
    #region private fields

    private readonly IResourceService _resourceService;
    private readonly IShipRepository _shipRepository;

    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ShipService(
        IResourceService resourceService,
        IShipRepository shipRepository,
        IMapper mapper,
        ILogger<ShipService> logger)
    {
        _resourceService = resourceService;
        _shipRepository = shipRepository;

        _mapper = mapper;
        _logger = logger;
    }

    #endregion

    #region public methods

    /// <inheritdoc/>
    public ShipDTO CreateShip()
    {
        _logger.LogInformation("Create space ship");

        Ship ship = _shipRepository.Create(
            new()
            {
                Step = 0,
                State = ShipState.OK,
            },
            saveChanges: false);

        ship.Resources = new List<Resource>()
        {
            new Resource()
            {
                Name = "Обшивка",
                Amount = 1,
                State = ResourceState.OK,
                ResourceType = ResourceType.Hull
            },
            new Resource()
            {
                Name = "Металлический лом",
                Amount = 4,
                State = ResourceState.OK,
                ResourceType = ResourceType.ScrapMetal
            },
            new Resource()
            {
                Name = "Двигатель (передний)",
                Amount = 1,
                State = ResourceState.OK,
                ResourceType = ResourceType.Engine
            },
            new Resource()
            {
                Name = "Двигатель (боковой)",
                Amount = 2,
                State = ResourceState.OK,
                ResourceType = ResourceType.Engine
            },
            new Resource()
            {
                Name = "Топливо",
                Amount = 22,
                State = ResourceState.OK,
                ResourceType = ResourceType.Fuel
            }
        };

        _shipRepository.SaveChanges();

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <inheritdoc/>
    public ShipDTO GetShip(Guid shipId)
    {
        _logger.LogInformation("Get space ship by id {id}", shipId);

        Ship ship = GetRepositoryShip(shipId);

        return _mapper.Map<ShipDTO>(ship);
    }

    /// <inheritdoc/>
    public ShipDTO ProcessNewDay(Guid shipId)
    {
        _logger.LogInformation("Processing new day on board the space ship with id {id}.", shipId);

        Ship ship = GetRepositoryShip(shipId);

        SpendShipResources(ship);

        TryFlyShip(ship);

        ship.Step++;

        _shipRepository.Update(ship, saveChanges: true);

        return GetShip(shipId);
    }

    /// <inheritdoc/>
    public bool DeleteShip(Guid shipId)
    {
        _logger.LogInformation("Delete space ship with id {id}", shipId);

        return _shipRepository.Delete(shipId);
    }

    #endregion

    #region private methods

    /// <summary>
    /// Get ship from repository.
    /// </summary>
    /// <exception cref="NotFoundException">
    /// In case the ship is not found by the repository.
    /// </exception>
    private Ship GetRepositoryShip(Guid shipId)
    {
        Ship? ship = _shipRepository.Get(shipId);

        if (ship == null)
        {
            _logger.LogInformation("Space ship with ID {shipId} not found.", shipId);

            throw new NotFoundException($"Ship with ID {shipId} not found.");
        }

        return ship;
    }

    /// <summary>
    /// Spend lifesupport for all resources on ship <paramref name="ship"/>.
    /// </summary>
    private void SpendShipResources(Ship ship)
    {
        foreach (Resource resource in ship.Resources)
        {
            ResourceType? requiredResourceType = _resourceService.GetRequiredResourceType(resource);

            if (requiredResourceType == null)
            {
                // Already logged.
                continue;
            }

            int requiredAmount = _resourceService.GetRequiredResourceAmount(resource);

            if (requiredAmount == 0)
            {
                continue;
            }

            List<Resource> requiredResources = ship.Resources
                .Where(x => x.ResourceType == requiredResourceType.Value)
                .ToList();

            if (requiredResources.Count == 0)
            {
                _logger.LogWarning(
                    "No resources of type {requiredResourceType} on board.",
                    requiredResourceType.Value);

                _resourceService.UpdateResourceState(resource, ResourceState.Fail);

                continue;
            }

            if (requiredResources.Select(x => x.Amount).Sum() < requiredAmount)
            {
                _logger.LogWarning(
                    "Insufficient resources of type {requiredResourceType} on board. All leftovers are spent.",
                    requiredResourceType.Value);

                requiredResources.ForEach(requiredResource => _resourceService.UpdateResourceAmount(requiredResource, 0));

                _resourceService.UpdateResourceState(resource, ResourceState.Fail);

                continue;
            }

            _logger.LogInformation(
                "Spending {requiredAmount} of {requiredResourceType} for {resourceType} life-support.",
                requiredAmount,
                requiredResourceType.Value,
                resource.ResourceType);

            int leftoverAmount = requiredAmount;
            foreach (Resource requiredResource in requiredResources)
            {
                if (requiredResource.Amount <= leftoverAmount)
                {
                    leftoverAmount -= requiredResource.Amount;
                    _resourceService.UpdateResourceAmount(requiredResource, 0);

                    continue;
                }

                _resourceService.UpdateResourceAmount(requiredResource, requiredResource.Amount - leftoverAmount);

                break;
            }
        }
    }

    /// <summary>
    /// Try moving the ship in space.
    /// </summary>
    private void TryFlyShip(Ship ship)
    {
        List<Resource> engines = ship.Resources.Where(x => x.ResourceType == ResourceType.Engine).ToList();

        if (engines.Count == 0 ||
            engines.All(x => x.State == ResourceState.Fail) ||
            engines.All(x => x.State == ResourceState.Sleep))
        {
            _logger.LogInformation("Ship {shipId} cannot fly without engines.", ship.Id);

            ship.State = ShipState.Adrift;

            return;
        }

        _logger.LogInformation("Ship flies!");

        ship.State = ShipState.OK;
    }

    #endregion
}
