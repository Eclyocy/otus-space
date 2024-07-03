using AutoMapper;
using GameController.API.Helpers;
using GameController.API.Models.User;
using GameController.Services.Models.User;

namespace GameController.API.Mappers
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
            // Controller models -> Service models
            CreateMap<CreateUserModel, CreateUserDto>()
                .ForMember(
                    x => x.PasswordHash,
                    opt => opt.MapFrom(x => PasswordConverter.ConvertToHash(x.Password)));
            CreateMap<UpdateUserModel, UpdateUserDto>()
                .ForMember(
                    x => x.PasswordHash,
                    opt => opt.MapFrom(x => x.Password == null ? null : PasswordConverter.ConvertToHash(x.Password)));

            // Service models -> Controller models
            CreateMap<UserDto, UserModel>();
        }
    }
}
