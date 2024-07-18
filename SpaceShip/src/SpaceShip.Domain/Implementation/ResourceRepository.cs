using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceRepository : IResourceRepository
    {
        private EfCoreContext _context;

        public ResourceRepository(EfCoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выборка ресурса по id
        /// </summary>
        /// <param name="id">ID ресурса</param>
        /// <returns>true если ресурс есть в БД</returns>
        public bool FindById(int id)
        {
            var resources = _context.Resources
              .Where(resources => resources.Id == id);

            if (resources == null)
            {
                return true;
            }

            return false;
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
            if (!FindById(resource.Id))
            {
                throw new Exception("Resource not found");
            }

            _context.Resources.Update(resource);

            return _context.Resources.Find(resource.Id) ?? throw new Exception("Resource not found");
        }
    }
}
