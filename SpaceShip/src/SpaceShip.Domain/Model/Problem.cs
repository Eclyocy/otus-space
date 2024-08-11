using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Проблемы.
    /// </summary>
    [Table("Problem")]
    public class Problem : BaseEntity
    {
        /// <summary>
        /// Имя проблемы.
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Коллекция типов ресурсов.
        /// </summary>
        public virtual ICollection<ResourceType>? ResourceTypes { get; set; }
    }
}
