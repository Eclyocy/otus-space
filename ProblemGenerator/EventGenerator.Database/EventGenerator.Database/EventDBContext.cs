using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventGenerator.Database
{
    /// <summary>
    /// Database context for event controller application.
    /// </summary>
    public class EventDBContext : DbContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Generator.
        /// </summary>
        public DbSet<Generator> Generators { get; set; }

        /// <summary>
        /// Events.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            Console.WriteLine(config.GetConnectionString("DefaultConnection"));

            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }
    }
}
