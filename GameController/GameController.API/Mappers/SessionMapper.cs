using AutoMapper;
using GameController.API.Models.Session;
using GameController.Services.Models.Session;

namespace GameController.API.Mappers
{
    /// <summary>
    /// Mappings for Session models.
    /// </summary>
    public class SessionMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateSessionRequest, CreateSessionDto>();

            // Service models -> Controller models
            CreateMap<SessionDto, SessionResponse>();
        }
    }
}
