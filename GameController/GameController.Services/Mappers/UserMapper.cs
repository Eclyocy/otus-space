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
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Database models -> Service models
            CreateMap<User, UserDto>();
        }
    }
}
