using AutoMapper;
using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Generator;

namespace EventGenerator.Services.Mappers
{
    /// <summary>
    /// Mappers for events generator models.
    /// </summary>
    public class GeneratorMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorMapper()
        {
            // Service models -> Database models
            CreateMap<CreateGeneratorDto, Generator>();

            // Database models -> Service models
            CreateMap<Generator, GeneratorDto>();
        }
    }
}
