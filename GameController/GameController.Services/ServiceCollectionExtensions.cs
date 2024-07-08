using GameController.Services.Interfaces;
using GameController.Services.Mappers;
using GameController.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GameController.Services
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> for services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region public methods

        /// <summary>
        /// Configure project-specific service collection with DI.
        /// </summary>
        public static IServiceCollection ConfigureApplicationServices(
            this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(RabbitMQMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(SessionMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(UserMapper)));

            services.AddTransient<IGeneratorService, GeneratorService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShipService, ShipService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRabbitMQService, RabbitMQService>();

            return services;
        }

        #endregion
    }
}
