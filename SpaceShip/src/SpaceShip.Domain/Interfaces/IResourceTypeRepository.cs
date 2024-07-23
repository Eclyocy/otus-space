using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория типа ресурсов.
    /// </summary>
    public interface IResourceTypeRepository : IRepository<ResourceType>
    {
        /// <summary>
        /// Выборка типа ресурса по id
        /// </summary>
        /// <param name="id">ID типа ресурса</param>
        /// <returns>true если тип ресурса есть в БД</returns>
        public bool FindById(Guid id);

        /// <summary>
        /// Метод создания типа ресурса.
        /// </summary>
        /// <returns>Проблема</returns>
        public ResourceType Create(string name);

        /// <summary>
        /// Получить существующий тип ресурса.
        /// </summary>
        /// <param name="id">ID ресурс.</param>
        /// <returns>Модель типа ресурса.</returns>
        public ResourceType Get(Guid id);

        /// <summary>
        /// Обновление существующего типа ресурса в БД
        /// </summary>
        /// <param name="resourceType">обновленная сущность</param>
        /// <returns>Модель типа ресурса</returns>
        public ResourceType Update(ResourceType resourceType);

        /// <summary>
        /// Delete entity by <paramref name="id"/>.
        /// </summary>
        /// <returns>The indication of operation success.</returns>
        bool Delete(Guid id);
    }
}
