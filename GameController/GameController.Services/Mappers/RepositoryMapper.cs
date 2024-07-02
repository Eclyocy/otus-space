using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Models.Message;
using GameController.Services.Models.Session;
using GameController.Services.Models.User;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappings for RepositoryModels
    /// </summary>
    public class RepositoryMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public RepositoryMapper()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.NameUser, opt => opt.MapFrom(x=>x.Name))
                .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash));

            CreateMap<UpdateUserDto, User>()
               .ForMember(x => x.NameUser, opt => opt.MapFrom(x => x.Name))
               .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash))
               .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id));

            CreateMap<User, UserDto>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(x => x.NameUser))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.UserId));
        }
    }
}
