namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Типы ресурсов.
    /// </summary>
    public class ResourceType : BaseEntity
    {
        /// <summary>
        /// ResourceType ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя типа ресурса.
        /// </summary>
        public string Name { get; set; }
    }
}
