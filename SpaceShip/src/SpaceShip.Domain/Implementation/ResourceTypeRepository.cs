using SpaceShip.Domain.EfCore;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.Implementation
{
    public class ResourceTypeRepository : IResourceTypeRepository
    {
        private EfCoreContext _context;

        public ResourceTypeRepository(EfCoreContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Выборка типа ресурса по id
        /// </summary>
        /// <param name="id">ID типа ресурса</param>
        /// <returns>true если тип ресурса есть в БД</returns>
        public bool FindById(int id)
        {
            var resourceType = _context.ResourcesType
              .Where(rT => rT.Id == id);

            if (resourceType == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод создания типа ресурса.
        /// </summary>
        /// <returns>Проблема</returns>
        public ResourceType Create(string name)
        {
            var newResourceType = new ResourceType() { Name = name };

            _context.Add(newResourceType);
            _context.SaveChanges();

            return newResourceType;
        }

        /// <summary>
        /// Получить существующий тип ресурса.
        /// </summary>
        /// <param name="id">ID ресурс.</param>
        /// <returns>Модель типа ресурса.</returns>
        public ResourceType Get(int id)
        {
            return _context.ResourcesType.Find(id) ?? throw new Exception("ResourceType not found");
        }

        /// <summary>
        /// Обновление существующего типа ресурса в БД
        /// </summary>
        /// <param name="resourceType">обновленная сущность</param>
        /// <returns>Модель типа ресурса</returns>
        public ResourceType Update(ResourceType resourceType)
        {
            if (!FindById(resourceType.Id))
            {
                throw new Exception("ResourceType not found");
            }

            _context.ResourcesType.Update(resourceType);

            return _context.ResourcesType.Find(resourceType.Id) ?? throw new Exception("ResourceType not found");
        }
    }
}
