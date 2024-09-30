using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Models.Generator;

namespace EventGenerator.API.Mappers
{
    /// <summary>
    /// Mappings for generator models.
    /// </summary>
    public class GeneratorMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratorMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateGeneratorRequest, CreateGeneratorDto>();

            // Service models -> Controller models
            CreateMap<GeneratorDto, GeneratorResponse>();
        }
    }
}
