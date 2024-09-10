using GameController.Services.Interfaces;
using GameController.Services.Mappers;
using GameController.Services.Services;
using GameController.Services.Settings;
using Microsoft.Extensions.Configuration;
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
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(RabbitMQMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(SessionMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(UserMapper)));

            services.Configure<RabbitMQSettings>(x => configuration.GetSection("RabbitMQ").Bind(x));
            services.Configure<SpaceShipApiSettings>(x => configuration.GetSection("SpaceShipApi").Bind(x));

            services.AddTransient<IGeneratorService, GeneratorService>();
            services.AddTransient<IRabbitMQService, RabbitMQService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IShipService, ShipService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }

        #endregion
    }
}
