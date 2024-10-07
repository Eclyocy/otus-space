using Microsoft.Extensions.DependencyInjection;
using SpaceShip.Domain.Implementation;
using SpaceShip.Domain.Interfaces;

namespace SpaceShip.Domain.ServiceCollectionExtensions
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
            services.AddDbContext<DatabaseContext>();

            services.AddTransient<IResourceRepository, ResourceRepository>();
            services.AddTransient<IShipRepository, ShipRepository>();

            return services;
        }

        #endregion
    }
}
