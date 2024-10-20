using AutoMapper;
using GameController.API.Models.Auth;
using GameController.Services.Models.Auth;

namespace GameController.API.Mappers
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<LoginRequest, LoginDto>();
            CreateMap<TokenResponseDto, TokenResponse>();
        }
    }
}
