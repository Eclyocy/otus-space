using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Проблемы.
    /// </summary>
    [Table("problem")]
    public class Problem : BaseEntity
    {
        /// <summary>
        /// Имя проблемы.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Коллекция типов ресурсов.
        /// </summary>
        public virtual ICollection<ResourceType> ResourceTypes { get; set; }
    }
}
