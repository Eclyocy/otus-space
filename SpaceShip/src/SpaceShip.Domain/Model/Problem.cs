namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Проблемы.
    /// </summary>
    public class Problem : BaseEntity
    {
        /// <summary>
        /// Problem ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя проблемы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция типов ресурсов.
        /// </summary>
        public virtual ICollection<ResourceType> ResourceTypes { get; set; }
    }
}
