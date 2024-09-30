using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EventGenerator.Database
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> for database.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure project-specific service collection with DI.
        /// </summary>
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services)
        {
            services.AddDbContext<EventDBContext>();

            services.AddTransient<IGeneratorRepository, GeneratorRepository>();
            services.AddTransient<IEventRepository, EventRepository>();

            return services;
        }
    }
}
