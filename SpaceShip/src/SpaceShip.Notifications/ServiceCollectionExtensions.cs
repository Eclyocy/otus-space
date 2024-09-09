using Microsoft.Extensions.DependencyInjection;

namespace SpaceShip.Notifications
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
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddTransient<INotificationsProvider, NotificationsProvider>();

            return services;
        }

        #endregion
    }
}
