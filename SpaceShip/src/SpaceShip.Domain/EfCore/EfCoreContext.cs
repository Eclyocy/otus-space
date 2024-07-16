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

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = "Server=127.0.0.1;Port=5432;Database=Spaceship;User Id=postgres;Password=123;";

                builder.UseNpgsql(conn);
            }

            base.OnConfiguring(builder);
        }
    }
}
