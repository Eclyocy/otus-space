using Microsoft.Extensions.Logging;
using Shared.Enums;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Builder.Abstractions;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Helpers;

namespace SpaceShip.Service.Builder;

/// <inheritdoc/>
public class ShipBuilder : IShipBuilder
{
    #region private fields

    private readonly string _requestId = Guid.NewGuid().ToString();
    private readonly List<ResourceDTO> _resources = new();

    private readonly IShipRepository _shipRepository;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    public ShipBuilder(
        IShipRepository repository,
        ILogger<ShipBuilder> logger)
    {
        _shipRepository = repository;
        _logger = logger;
    }

    #endregion

    #region public properties

    /// <summary>
    /// Ship name. If null will be generated.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Ship state. Default Ok.
    /// </summary>
    public ShipState? State { get; set; }

    /// <summary>
    /// Resources.
    /// </summary>
    public List<ResourceDTO> Resources { get => _resources; }

    #endregion

    #region public methods

    /// <inheritdoc/>
    public Ship Build()
    {
        _logger.LogTrace("[{id}] Trying to create ship.", _requestId);
        var ship = _shipRepository.Create(new Ship()
        {
            Name = Name ?? RandomNameGenerator.Get(),
            Step = 0,
            State = ShipState.OK,
            DistanceTraveled = 0,
            DistanceTarget = 10
        });

        _logger.LogTrace("[{id}] Trying to create base resources.", _requestId);
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
                Name = "Двигатель",
                Amount = 1,
                State = ResourceState.OK,
                ResourceType = ResourceType.Engine
            },
            new Resource()
            {
                Name = "Топливо",
                Amount = 22,
                State = ResourceState.OK,
                ResourceType = ResourceType.Fuel
            },
            new Resource()
            {
                Name = "Металл",
                Amount = 10,
                State = ResourceState.OK,
                ResourceType = ResourceType.ScrapMetal
            }
        };

        _logger.LogTrace("[{id}] Trying to create additional resources.", _requestId);
        foreach (var resource in _resources)
        {
            ship.Resources.Add(
                new Resource()
                {
                    Name = resource.Name,
                    Amount = resource.Amount,
                    State = ResourceState.OK,
                    ResourceType = resource.ResourceType
                });
        }

        _logger.LogTrace("[{id}] Saving changes in ship repository.", _requestId);
        _shipRepository.SaveChanges();

        _logger.LogInformation(
            "[{id}] Building ship [{shipId}] complete.",
            _requestId,
            ship.Id);

        return ship;
    }

    /// <inheritdoc/>
    public IShipBuilder AddResource(ResourceDTO resource)
    {
        _logger.LogInformation(
            "[{id}] Add new resource to ship creation request. Resource type [{type}], name [{name}], quantity [{quantity}]",
            _requestId,
            resource.ResourceType,
            resource.Name,
            resource.Amount);

        _resources.Add(resource);
        return this;
    }

    #endregion
}
