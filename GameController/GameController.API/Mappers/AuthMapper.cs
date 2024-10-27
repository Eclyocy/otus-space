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
            CreateMap<TokenRequest, TokenDto>();
            CreateMap<TokenResponseDto, TokenResponse>();
            CreateMap<TokenResponseDto, TokenRequest>()
           .ForMember(
                x => x.Token,
                opt => opt.MapFrom(src => src.AccessToken))
           .ForMember(
                x => x.RefreshToken,
                opt => opt.MapFrom(src => src.RefreshToken));
        }
    }
}
