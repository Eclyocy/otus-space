﻿using AutoMapper;
using GameController.API.Models.Auth;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameController.API.Controllers
{
    /// <summary>
    /// Controller for user authorization.
    /// </summary>
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        #region private fields

        private readonly IAuthService _authService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthController(
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;

            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Log-in the user.
        /// </summary>
        [HttpPost]
        [Route("login")]
        [SwaggerOperation("Аутентификация пользователя")]
        public IActionResult Login(LoginRequest login)
        {
            TokenDto result = _authService.Authenticate(_mapper.Map<LoginDto>(login));
            return Ok(_mapper.Map<TokenResponse>(result));
        }

        /// <summary>
        /// Refresh user login token.
        /// </summary>
        [HttpPost]
        [Route("refresh")]
        [SwaggerOperation("Обновление пользовательского токена")]
        public IActionResult Refresh(RefreshTokenRequest tokenModel)
        {
            TokenDto result = _authService.RefreshToken(_mapper.Map<RefreshTokenDto>(tokenModel));
            return Ok(_mapper.Map<TokenResponse>(result));
        }

        /// <summary>
        /// Test authorization.
        /// </summary>
        [HttpGet]
        [Route("test")]
        [Authorize]
        public IActionResult TestAuth()
        {
            string? userId = User.Identity?.Name;

            return Ok(string.Format(
                "Welcome {0}, you are authorized to play!",
                string.IsNullOrEmpty(userId) ? "Anonymous" : userId));
        }

        #endregion
    }
}
