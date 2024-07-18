using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория ресурсов.
    /// </summary>
    public interface IResourceRepository
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
        public bool FindById(int id);

        /// <summary>
        /// Получить существующий ресурс.
        /// </summary>
        /// <param name="id">ID ресурс.</param>
        /// <returns>Модель ресурса.</returns>
        public Resource Get(int id);

        /// <summary>
        /// Обновление существующего ресурса в БД
        /// </summary>
        /// <param name="resource">обновленная сущность</param>
        /// <returns>Модель ресурса</returns>
        public Resource Update(Resource resource);
    }
}
