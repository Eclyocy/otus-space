using Microsoft.Extensions.Logging;
using Shared.Enums;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Builder.Abstractions;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Helpers.Abstractions;

namespace SpaceShip.Service.Builder;

/// <summary>
/// Build spaceship with specified resources.
/// </summary>
public class ShipBuilder : IShipBuilder
{
    #region private fields

    private readonly string _requestId = Guid.NewGuid().ToString();
    private readonly List<ResourceDTO> _resources = new List<ResourceDTO>();

    private readonly IShipRepository _shipRepository;
    private readonly INameGenerator _nameGenerator;
    private readonly ILogger _logger;

    #endregion

    #region constructor

    public ShipBuilder(IShipRepository repository, INameGenerator nameGenerator, ILogger<ShipBuilder> logger)
    {
        _shipRepository = repository;
        _nameGenerator = nameGenerator;
        _logger = logger;
    }

    #endregion

    #region  public properties

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

    /// <summary>
    /// Applying new resource to shaceship resource collection.
    /// </summary>
    /// <param name="resource">Resource to add.</param>
    /// <returns>Builder.</returns>
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

    /// <summary>
    /// Create ship with resources by specified builder parameters.
    /// </summary>
    /// <returns>Ship.</returns>
    public Ship Build()
    {
        try
        {
            _logger.LogTrace("[{id}] Trying to create ship.", _requestId);
            var ship = _shipRepository.Create(new Ship()
            {
                Name = Name ?? _nameGenerator.Get(),
                Step = 0,
                State = ShipState.OK
            });

            _logger.LogTrace("[{id}] Trying to create base resources.", _requestId);
            ship.Resources = ship.Resources = new List<Resource>()
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
                }
            };

            _logger.LogTrace("[{id}] Trying to create additional resources.", _requestId);
            foreach (var res in _resources)
            {
                ship.Resources.Add(
                    new Resource()
                    {
                        Name = res.Name,
                        Amount = res.Amount,
                        State = ResourceState.OK,
                        ResourceType = res.ResourceType
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
        catch
        {
            _logger.LogError("[{id}] Fail to build new spaceship.", _requestId);
            throw;
        }
    }

    #endregion
}
