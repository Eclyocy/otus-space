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
        /// Events.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Ships.
        /// </summary>
        public DbSet<Ship> Ships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            Console.WriteLine(config.GetConnectionString("DefaultConnection"));

            optionsBuilder.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name)
                ?? throw new Exception(string.Format("{0} environment variable must be specified", name));
        }
    }
}
