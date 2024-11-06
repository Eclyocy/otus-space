using System.ComponentModel.DataAnnotations.Schema;
using Shared.Enums;

namespace SpaceShip.Domain.Entities;

/// <summary>
/// Space ship entity.
/// </summary>
[Table("Ship")]
public class Ship : BaseEntity
{
    /// <summary>
    /// Space ship name.
    /// </summary>
    [Column("Name")]
    public string? Name { get; set; }

    /// <summary>
    /// Space ship state.
    /// </summary>
    [Column("State")]
    public ShipState State { get; set; }

    /// <summary>
    /// Number of day on board the space ship.
    /// </summary>
    [Column("Step")]
    public short Step { get; set; }

    /// <summary>
    /// Value indicating the distance the space ship has traveled.
    /// </summary>
    [Column("DistanceTraveled")]
    public byte DistanceTraveled { get; set; }

    /// <summary>
    /// Value indicating the target distance the space ship has to cover.
    /// </summary>
    [Column("DistanceTarget")]
    public byte DistanceTarget { get; set; }

    /// <summary>
    /// Related resources collection.
    /// </summary>
    public virtual ICollection<Resource> Resources { get; set; }
}
