namespace Spaceship.DataLayer.EfClasses
{
    public class Problem
    {
        public int Id { get; set; }
        public int ResourceTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ResourceType> ResourcesType { get; set; }
    }
}
