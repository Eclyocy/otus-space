using Spaceship.DataLayer.EfClasses;


namespace DataLayer.DTO
{
    public class ProblemDataDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ResourceType> ResourceTypes { get; set; }
    }
}
