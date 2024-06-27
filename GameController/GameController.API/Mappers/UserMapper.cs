using AutoMapper;
using GameController.API.Models.User;
using GameController.Services.Models.User;

namespace GameController.API.Mappers
{
    /// <summary>
    /// Mappings for User models.
    /// </summary>
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            // Controller models -> Service models
            CreateMap<CreateUserModel, CreateUserDto>();
            CreateMap<UpdateUserModel, UpdateUserDto>();

            // Service models -> Controller models
            CreateMap<UserDto, UserModel>();
        }
    }
}
