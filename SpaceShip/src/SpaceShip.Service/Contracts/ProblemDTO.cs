using SpaceShip.Domain.Model;

namespace SpaceShip.Service.Contracts
{
    public class ProblemDTO
    {
        /// <summary>
        /// Id проблемы
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование проблемы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция типов ресурсов
        /// </summary>
        public ICollection<ResourceType> ResourceTypes { get; set; }
    }
}
