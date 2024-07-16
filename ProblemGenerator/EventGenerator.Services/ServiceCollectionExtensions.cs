using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Mappers;
using EventGenerator.Services.Services;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventGenerator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(
            this IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(EventMapper)));

            services.AddTransient<IGeneratorService, GeneratorService>();
           
            return services;
        }
    }
}
