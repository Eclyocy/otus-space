using SpaceShip.Domain.DTO;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория космических кораблей.
    /// </summary>
    public interface ISpaceshipRepository : IRepository<SpaceShipModelDto>
    {
        /// <summary>
        /// Выборка корабля по id
        /// </summary>
        /// <param name="id">ID космического корабля</param>
        /// <returns>true если корабль есть в БД</returns>
        public bool FindById(Guid id);

        /// <summary>
        /// Метод создания корабля.
        /// </summary>
        /// <returns>Корабль</returns>
        public Ship Create();

        /// <summary>
        /// Получить существующий корабль.
        /// </summary>
        /// <param name="id">ID корабля.</param>
        /// <returns>Модель корабля.</returns>
        public Ship Get(Guid id);

        /// <summary>
        /// Обновление существующего корабля в БД
        /// </summary>
        /// <param name="ship">обновленная сущность</param>
        /// <returns>Модель корабля</returns>
        public Ship Update(Ship ship);

        /// <summary>
        /// Delete entity by <paramref name="id"/>.
        /// </summary>
        /// <returns>The indication of operation success.</returns>
        bool Delete(Guid id);
    }
}
