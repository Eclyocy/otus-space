using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace SpaceShip.Domain.Entities;

/// <summary>
/// Resource entity.
/// </summary>
[Table("Resource")]
public class Resource : BaseEntity
{
    /// <summary>
    /// ID of related space ship entity.
    /// </summary>
    [Column("ShipId")]
    [Required]
    public Guid ShipId { get; set; }

    /// <summary>
    /// Resource type.
    /// </summary>
    [Column("ResourceType")]
    [Required]
    public ResourceType ResourceType { get; set; }

    /// <summary>
    /// Resource state.
    /// </summary>
    [Column("State")]
    [Required]
    public ResourceState State { get; set; }

    /// <summary>
    /// State criticality level.
    /// </summary>
    [Column("StateCriticality")]
    public EventLevel? StateCriticality { get; set; }

    /// <summary>
    /// Resource name.
    /// </summary>
    [Column("Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Resource amount.
    /// </summary>
    [Column("Amount")]
    [Required]
    public int Amount { get; set; }

    /// <summary>
    /// Related entity: space ship.
    /// </summary>
    [ForeignKey("ShipId")]
    public virtual Ship Ship { get; set; }

    /// <summary>
    /// Resource type required for this resource life-support.
    /// </summary>
    public ResourceType? RequiredResourceType => ResourceType switch
    {
        ResourceType.Hull => ResourceType.ScrapMetal,
        ResourceType.Engine => ResourceType.Fuel,
        ResourceType.ScrapMetal => null,
        ResourceType.Fuel => null,
        _ => null
    };

    /// <summary>
    /// Resource type required for this resource repair.
    /// </summary>
    public (ResourceType? ResourceType, int Amount) SpareResourceType
    {
        get
        {
            if (State != ResourceState.Fail)
            {
                return (null, 0); // No needs to repair.
            }

            return (ResourceType, StateCriticality) switch
            {
                (ResourceType.Hull, EventLevel.Low) => (ResourceType.ScrapMetal, 1),
                (ResourceType.Hull, EventLevel.Medium) => (ResourceType.ScrapMetal, 2),
                (ResourceType.Hull, EventLevel.High) => (ResourceType.ScrapMetal, 3),
                (ResourceType.Engine, _) => (ResourceType.ScrapMetal, 1),
                (ResourceType.ScrapMetal, _) => (null, 0),
                (ResourceType.Fuel, _) => (null, 0),
                _ => (null, 0)
            };
        }
    }
}
