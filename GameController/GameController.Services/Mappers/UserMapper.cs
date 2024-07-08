using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Models.User;

namespace GameController.Services.Mappers
{
    /// <summary>
    /// Mappings for User models.
    /// </summary>
    public class UserMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserMapper()
        {
            // Service models -> Database models
            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.NameUser, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash));

            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.NameUser, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.HashPass, opt => opt.MapFrom(x => x.PasswordHash));

            // Database models -> Service models
            CreateMap<User, UserDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.NameUser));
        }
    }
}
