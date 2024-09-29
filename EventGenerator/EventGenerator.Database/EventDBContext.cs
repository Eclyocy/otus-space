using EventGenerator.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EventGenerator.Database
{
    /// <summary>
    /// Database context for event generator application.
    /// </summary>
    public class EventDBContext : DbContext
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Generator.
        /// </summary>
        public DbSet<Generator> Generators { get; set; }

        /// <summary>
        /// Events.
        /// </summary>
        public DbSet<Event> Events { get; set; }

        #endregion

        #region protected methods

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

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Generator>()
                .HasMany(g => g.Events)
                .WithOne(e => e.Generator)
                .HasForeignKey(e => e.GeneratorId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Generator)
                .WithMany(g => g.Events);
        }

        #endregion

        #region private methods

        private static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name)
                ?? throw new Exception(string.Format("{0} environment variable must be specified", name));
        }

        #endregion
    }
}
