using SpaceShip.Domain.Model.State;

namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Ресурсы.
    /// </summary>
    public class Resource : BaseEntity
    {
        /// <summary>
        /// Resource ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// SpaceshipId.
        /// </summary>
        public Guid SpaceshipId { get; set; }

        /// <summary>
        /// ResourceTypeId.
        /// </summary>
        public Guid ResourceTypeId { get; set; }

        /// <summary>
        /// Статус ресурса.
        /// </summary>
        public ResourceState State { get; set; }

        /// <summary>
        /// Название ресурса.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество ресурса.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Виртуцальное ствойство корабля.
        /// </summary>
        public virtual Ship Spaceship { get; set; }

        /// <summary>
        /// Виртуцальное ствойство типа ресурса.
        /// </summary>
        public virtual ResourceType ResourceType { get; set; }
    }
}
