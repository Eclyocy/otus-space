using AutoMapper;
using GameController.API.Models.Auth;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameController.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            TokenResponseDto result = _authService.ValidateUser(_mapper.Map<LoginDto>(login));
            return Ok(_mapper.Map<TokenResponse>(result));
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenRequest tokenModel)
        {
            TokenResponseDto result = _authService.RefreshToken(_mapper.Map<TokenDto>(tokenModel));
            return Ok(_mapper.Map<TokenRequest>(result));
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult TestAuth()
        {
            var username = User.Identity.Name;
            return Ok($"Welcome {username}, you are authorized to play!");
        }
    }
}
