using Microsoft.Extensions.DependencyInjection;
using SpaceShip.Service.Builder;
using SpaceShip.Service.Builder.Abstractions;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Services;

namespace SpaceShip.Service.Extensions;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> for services.
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
        services.AddTransient<IResourceService, ResourceService>();
        services.AddTransient<IShipBuilder, ShipBuilder>();
        services.AddTransient<IShipService, ShipService>();

        return services;
    }

    #endregion
}
