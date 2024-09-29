using GameController.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace GameController.Database
{
    /// <summary>
    /// Database context for game controller application.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Sessions.
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

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
