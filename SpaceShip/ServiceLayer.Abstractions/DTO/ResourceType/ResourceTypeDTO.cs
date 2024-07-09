using ServiceLayer.Abstractions.ReturnResult;

namespace Spaceship.DataLayer.EfClasses
{
    public class ResourceTypeDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ResourceTypeDTO()
        {
            
        }
    }
}
