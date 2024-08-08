using Microsoft.EntityFrameworkCore;
using SpaceShip.Domain.Model;

namespace SpaceShip.Domain.EfCore
{
    /// <summary>
    /// Database context for game controller application.
    /// </summary>
    public class EfCoreContext : DbContext
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EfCoreContext()
        {
        }

        #endregion

        #region public properties

        /// <summary>
        /// Users.
        /// </summary>
        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<Problem> Problems { get; set; }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<Resource> Resources { get; set; }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<ResourceType> ResourcesType { get; set; }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<Ship> Spaceships { get; set; }

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
