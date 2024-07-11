using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Models.Session;
using GameController.Services.Models.User;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappings for RepositoryModels.
    /// </summary>
    public class RepositoryMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryMapper()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            CreateMap<User, UserDto>();
            CreateMap<Session, SessionDto>()
                .ForMember(x => x.SessionId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
