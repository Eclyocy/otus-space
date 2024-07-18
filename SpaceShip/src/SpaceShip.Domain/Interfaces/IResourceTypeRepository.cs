using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория типа ресурсов.
    /// </summary>
    public interface IResourceTypeRepository
    {
        /// <summary>
        /// Выборка типа ресурса по id
        /// </summary>
        /// <param name="id">ID типа ресурса</param>
        /// <returns>true если тип ресурса есть в БД</returns>
        public bool FindById(int id);

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
        public ResourceType Get(int id);

        /// <summary>
        /// Обновление существующего типа ресурса в БД
        /// </summary>
        /// <param name="resourceType">обновленная сущность</param>
        /// <returns>Модель типа ресурса</returns>
        public ResourceType Update(ResourceType resourceType);
    }
}
