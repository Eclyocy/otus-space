using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceShip.Domain.Entities;

/// <summary>
/// Base entity.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Entity ID.
    /// </summary>
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }
}
