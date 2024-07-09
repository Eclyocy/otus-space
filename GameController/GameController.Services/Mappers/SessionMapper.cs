using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Models.Session;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappers for session models.
    /// </summary>
    public class SessionMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionMapper()
        {
            // Service models -> Database models
            CreateMap<CreateSessionDto, Session>();

            // Database models -> Service models
            CreateMap<Session, SessionDto>()
                .ForMember(x => x.SessionId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
