using AutoMapper;
using GameController.Database.Models;
using GameController.Services.Helpers;
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
                .ForMember(x => x.PasswordHash, opt => opt.MapFrom(src => HashHelper.HashPassword(src.PasswordHash)));
            CreateMap<UpdateUserDto, User>();

            // Database models -> Service models
            CreateMap<User, UserDto>();
        }
    }
}
