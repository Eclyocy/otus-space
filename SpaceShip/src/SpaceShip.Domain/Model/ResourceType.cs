using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Типы ресурсов.
    /// </summary>
    [Table("ResourceType")]
    public class ResourceType : BaseEntity
    {
        /// <summary>
        /// Имя типа ресурса.
        /// </summary>
        [Column("Name")]
        public string? Name { get; set; }
    }
}
