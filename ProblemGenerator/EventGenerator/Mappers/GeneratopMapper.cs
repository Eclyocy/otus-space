using AutoMapper;
using EventGenerator.API.Models;
using EventGenerator.Services.Models.Generator;

namespace EventGenerator.API.Mappers
{
    /// <summary>
    /// Mappings for generatop models.
    /// </summary>
    public class GeneratopMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GeneratopMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateGeneratorRequest, CreateGeneratorDto>();

            // Service models -> Controller models
            CreateMap<GeneratorDto, GeneratorResponse>();
        }
    }
}
