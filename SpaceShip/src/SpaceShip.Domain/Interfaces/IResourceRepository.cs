using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория ресурсов.
    /// </summary>
    public interface IResourceRepository : IRepository<Resource>
    {
        /// <summary>
        /// Метод создания ресурсов.
        /// </summary>
        /// <returns>Проблема</returns>
        public Resource Create();

        /// <summary>
        /// Выборка ресурса по id
        /// </summary>
        /// <param name="id">ID ресурса</param>
        /// <returns>true если ресурс есть в БД</returns>
        public bool FindById(Guid id);

        /// <summary>
        /// Получить существующий ресурс.
        /// </summary>
        /// <param name="id">ID ресурс.</param>
        /// <returns>Модель ресурса.</returns>
        public Resource Get(Guid id);

        /// <summary>
        /// Обновление существующего ресурса в БД
        /// </summary>
        /// <param name="resource">обновленная сущность</param>
        /// <returns>Модель ресурса</returns>
        public Resource Update(Resource resource);

        /// <summary>
        /// Delete entity by <paramref name="id"/>.
        /// </summary>
        /// <returns>The indication of operation success.</returns>
        bool Delete(Guid id);
    }
}
