using Microsoft.EntityFrameworkCore;
using Spaceship.DataLayer.EfClasses;

namespace DataLayer.EfCode
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
        public DbSet<Spaceship.DataLayer.EfClasses.Spaceship> Spaceships { get; set; }


    }
}

