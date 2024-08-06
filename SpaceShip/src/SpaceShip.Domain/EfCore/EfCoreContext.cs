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
    }
}
