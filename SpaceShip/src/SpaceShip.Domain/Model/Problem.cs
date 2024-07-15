namespace SpaceShip.Domain.Model
{
    public class Problem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ResourceType> ResourceTypes { get; set; }
    }
}
