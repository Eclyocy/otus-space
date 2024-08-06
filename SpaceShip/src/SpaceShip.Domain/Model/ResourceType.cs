namespace SpaceShip.Domain.Model
{
    /// <summary>
    /// Типы ресурсов.
    /// </summary>
    public class ResourceType : BaseEntity
    {
        /// <summary>
        /// Имя типа ресурса.
        /// </summary>
        public string Name { get; set; }
    }
}
