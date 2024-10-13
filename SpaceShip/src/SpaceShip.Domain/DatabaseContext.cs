using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.Entities;

namespace SpaceShip.Domain
{
    /// <summary>
    /// Database context for space ship application.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseContext()
        {
        }

        /// <summary>
        /// Constructor which ensures that the database is created.
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Resources.
        /// </summary>
        public DbSet<Resource> Resources { get; set; }

        /// <summary>
        /// Space ships.
        /// </summary>
        public DbSet<Ship> Ships { get; set; }

        #endregion

        #region protected methods

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string hostname = GetEnvironmentVariable("POSTGRES_HOST");
            string port = GetEnvironmentVariable("POSTGRES_PORT");
            string username = GetEnvironmentVariable("POSTGRES_USER");
            string password = GetEnvironmentVariable("POSTGRES_PASSWORD");
            string database = GetEnvironmentVariable("POSTGRES_DATABASE");

            string connectionString = string.Format(
                "Host={0};Port={1};Username={2};Password={3};Database={4};",
                hostname,
                port,
                username,
                password,
                database);

            optionsBuilder.UseNpgsql(connectionString);
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
