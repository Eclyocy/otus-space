using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceRepository : BaseRepository<Problem>, IResourceRepository
    {
        private EfCoreContext _context;

        public ResourceRepository(EfCoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод создания ресурсов.
        /// </summary>
        /// <returns>Проблема</returns>
        public Resource Create()
        {
            var newResource = new Resource();

            _context.Add(newResource);
            _context.SaveChanges();

            return newResource;
        }

        /// <summary>
        /// Получить существующий ресурс.
        /// </summary>
        /// <param name="id">ID ресурс.</param>
        /// <returns>Модель ресурса.</returns>
        public Resource Get(int id)
        {
            return _context.Resources.Find(id) ?? throw new Exception("Resource not found");
        }

        /// <summary>
        /// Обновление существующего ресурса в БД
        /// </summary>
        /// <param name="resource">обновленная сущность</param>
        /// <returns>Модель ресурса</returns>
        public Resource Update(Resource resource)
        {
            _context.Resources.Update(resource);

            return _context.Resources.Find(resource.Id) ?? throw new Exception("Resource not found");
        }
    }
}
