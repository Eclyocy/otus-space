using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;

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
            string hostname = GetEnvironmentVariable("DATABASE_HOSTNAME");
            string port = GetEnvironmentVariable("DATABASE_PORT");
            string username = GetEnvironmentVariable("DATABASE_USER");
            string password = GetEnvironmentVariable("DATABASE_PASSWORD");
            string database = GetEnvironmentVariable("DATABASE_NAME");

            string connectionString = string.Format(
                "Host={0};Port={1};Username={2};Password={3};Database={4};",
                hostname,
                port,
                username,
                password,
                database);

            optionsBuilder.UseNpgsql(connectionString);
        }

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name)
                ?? throw new Exception(string.Format("{0} environment variable must be specified", name));
        }
    }
}
