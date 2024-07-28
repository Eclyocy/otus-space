using Microsoft.Extensions.DependencyInjection;
using EventGenerator.Database.Repository;
using EventGenerator.Database.Interfaces;

namespace EventGenerator.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services)
        {
            services.AddDbContext<EventDBContext>();

            services.AddTransient<IEventRepository, EventRepository>();

            return services;
        }
    }
}
