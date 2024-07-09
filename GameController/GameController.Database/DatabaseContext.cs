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
    }
}
