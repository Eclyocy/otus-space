using Microsoft.Extensions.DependencyInjection;
using SpaceShip.Service.Builder;
using SpaceShip.Service.Builder.Abstractions;
using SpaceShip.Service.Helpers;
using SpaceShip.Service.Helpers.Abstractions;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Services;

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
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddTransient<IShipService, ShipService>();
            services.AddTransient<IResourceService, ResourceService>();
            services.AddTransient<INameGenerator, RandomNameGenerator>();
            services.AddTransient<IShipBuilder, ShipBuilder>();

            return services;
        }

        #endregion
    }
}
