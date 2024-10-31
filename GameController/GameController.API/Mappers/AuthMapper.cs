using AutoMapper;
using GameController.API.Models.Auth;
using GameController.Services.Models.Auth;

namespace GameController.API.Mappers
{
    /// <summary>
    /// Mappers for authorization models.
    /// </summary>
    public class AuthMapper : Profile
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthMapper()
        {
            // Controller models -> Service models
            CreateMap<LoginRequest, LoginDto>();
            CreateMap<RefreshTokenRequest, RefreshTokenDto>();

            // Service models -> Controller models
            CreateMap<TokenDto, TokenResponse>();
        }
    }
}
