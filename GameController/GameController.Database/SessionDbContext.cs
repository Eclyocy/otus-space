using GameController.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database
{
    /// <summary>
    /// SessionDbContext class.
    /// </summary>
    public class SessionDbContext : DbContext
    {
        /// <summary>
        /// SessionDbContext constructor.
        /// </summary>
        public SessionDbContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgresmaster");
        }
    }
}
