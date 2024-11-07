using EventGenerator.Services.Builders;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Mappers;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventGenerator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(GeneratorMapper)));
            services.AddAutoMapper(x => x.AddProfile(typeof(EventMapper)));

            services.AddSingleton<Random>();
            services.AddTransient<IEventBuilder, EventBuilder>();

            services.AddTransient<IGeneratorService, GeneratorService>();
            services.AddTransient<IEventService, EventService>();

            services.AddHostedService<RabbitMQService>();

            return services;
        }
    }
}
