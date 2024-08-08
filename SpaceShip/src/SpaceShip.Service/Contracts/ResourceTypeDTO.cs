namespace SpaceShip.Service.Contracts
{
    /// <summary>
    /// Model for resourceType ship.
    /// </summary>
    public class ResourceTypeDTO
    {
        /// <summary>
        /// Id типа ресурсов
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование ресурса
        /// </summary>
        public string Name { get; set; }
    }
}
