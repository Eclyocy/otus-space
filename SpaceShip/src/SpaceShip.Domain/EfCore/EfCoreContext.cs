using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.EfCore
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext()
        {
        }

        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceType> ResourcesType { get; set; }
        public DbSet<Ship> Spaceships { get; set; }
    }
}
