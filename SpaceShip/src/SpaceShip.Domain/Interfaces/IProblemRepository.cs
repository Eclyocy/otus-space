using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория проблем.
    /// </summary>
    public interface IProblemRepository
    {
        /// <summary>
        /// Метод создания проблемы.
        /// </summary>
        /// <returns>Проблема</returns>
        public Problem Create(string name);

        /// <summary>
        /// Выборка проблемы по id
        /// </summary>
        /// <param name="id">ID проблемы</param>
        /// <returns>true если проблема есть в БД</returns>
        public bool FindById(int id);

        /// <summary>
        /// Получить существующую проблему.
        /// </summary>
        /// <param name="id">ID проблемы.</param>
        /// <returns>Модель проблемы.</returns>
        public Problem Get(int id);

        /// <summary>
        /// Обновление существующей проблемы в БД
        /// </summary>
        /// <param name="problem">обновленная сущность</param>
        /// <returns>Модель проблемы</returns>
        public Problem Update(Problem problem);
    }
}
