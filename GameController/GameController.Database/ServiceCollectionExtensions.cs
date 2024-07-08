using GameController.Database.Interfaces;
using GameController.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameController.Database
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> for database.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region public methods

        /// <summary>
        /// Configure project-specific service collection with DI.
        /// </summary>
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgresmaster;Database=postgres;"));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();

            return services;
        }

        #endregion
    }
}
