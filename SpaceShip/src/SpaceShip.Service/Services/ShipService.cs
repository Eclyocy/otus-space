using AutoMapper;
using Microsoft.Extensions.Logging;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Enums;
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

    private readonly IShipRepository _shipRepository;

    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    /// <summary>
    /// Конструктор.
    /// </summary>
    public ShipService(
        IShipRepository shipRepository,
        IMapper mapper,
        ILogger<ShipService> logger)
    {
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

        foreach (Resource resource in ship.Resources)
        {
            if (resource.RequiredResourceType == null)
            {
                _logger.LogInformation(
                    "Resource {resourceName} of type {resourceType} is self-sufficient.",
                    resource.Name,
                    resource.ResourceType);

                continue;
            }

            int requiredAmount = resource.Amount;
            _logger.LogInformation(
                "Resource {resourceName} of type {resourceType} requires {requiredAmount} of {requiredResourceType} resource(s).",
                resource.Name,
                resource.ResourceType,
                requiredAmount,
                resource.RequiredResourceType);

            List<Resource> requiredResources = ship.Resources
                .Where(x => x.ResourceType == resource.RequiredResourceType.Value)
                .ToList();

            if (requiredResources.Count == 0)
            {
                _logger.LogWarning(
                    "No resources of type {requiredResourceType} on board. Resource {resourceName} is failed.",
                    resource.RequiredResourceType,
                    resource.Name);

                resource.State = ResourceState.Fail;

                continue;
            }

            if (requiredResources.Select(x => x.Amount).Sum() < requiredAmount)
            {
                _logger.LogWarning(
                    "Insufficient resources of type {requiredResourceType} on board. Resource {resourceName} spends all leftovers and is failed.",
                    resource.RequiredResourceType,
                    resource.Name);

                requiredResources.ForEach(x => x.Amount = 0);

                resource.State = ResourceState.Fail;

                continue;
            }

            _logger.LogInformation(
                "Spending {requiredAmount} of {requiredResourceType} for {resourceType} life-support.",
                requiredAmount,
                resource.RequiredResourceType,
                resource.ResourceType);

            int leftoverAmount = requiredAmount;
            foreach (Resource requiredResource in requiredResources)
            {
                if (leftoverAmount == 0)
                {
                    break;
                }

                if (requiredResource.Amount <= leftoverAmount)
                {
                    leftoverAmount -= requiredResource.Amount;
                    requiredResource.Amount = 0;

                    continue;
                }

                requiredResource.Amount -= leftoverAmount;
                leftoverAmount = 0;
            }
        }

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

    #endregion
}
