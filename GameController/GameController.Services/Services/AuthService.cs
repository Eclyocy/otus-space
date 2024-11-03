using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using GameController.Services.Exceptions;
using GameController.Services.Helpers;
using GameController.Services.Interfaces;
using GameController.Services.Models.Auth;
using GameController.Services.Models.User;
using GameController.Services.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GameController.Services.Services
{
    /// <summary>
    /// Service supplying authorization logic.
    /// </summary>
    public class AuthService : IAuthService
    {
        #region private fields

        private readonly IClaimsService _claimsService;
        private readonly IUserService _userService;

        private readonly JwtSettings _jwtSettings;

        private static readonly DateTime _epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AuthService(
            IClaimsService claimsService,
            IUserService userService,
            IOptions<JwtSettings> options)
        {
            _claimsService = claimsService;
            _userService = userService;

            _jwtSettings = options.Value;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public TokenDto Authenticate(LoginDto loginDto)
        {
            UserDto user = _userService.GetUserByName(loginDto.Username);

            if (HashHelper.HashPassword(loginDto.Password) != user.PasswordHash)
            {
                throw new UnauthorizedException($"Incorrect password supplied for user \"{loginDto.Username}\".");
            }

            return GenerateTokens(user);
        }

        /// <inheritdoc/>
        public TokenDto RefreshToken(RefreshTokenDto tokenDto)
        {
            string nameClaim = GetNameClaim(tokenDto.Token);

            if (!Guid.TryParse(nameClaim, out Guid userId))
            {
                throw new UnauthorizedException($"Invalid name claim supplied: {nameClaim}");
            }

            UserDto user;

            try
            {
                user = _userService.GetUser(userId);
            }
            catch (NotFoundException e)
            {
                throw new UnauthorizedException(e.Message);
            }

            return GenerateTokens(user);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Generate JWT for the user.
        /// </summary>
        private TokenDto GenerateTokens(UserDto user)
        {
            JwtSecurityToken token = GenerateToken(user);
            string refreshToken = GenerateRefreshToken();
            int expiresIn = (int)(token.ValidTo - _epoch).TotalSeconds;

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            };
        }

        /// <summary>
        /// Generate the refresh token.
        /// </summary>
        private static string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Generate the token.
        /// </summary>
        private JwtSecurityToken GenerateToken(UserDto user)
        {
            List<Claim> claims = _claimsService.GetClaims(user);

            SymmetricSecurityKey securityKey = AuthHelper.GetSymmetricSecurityKey(_jwtSettings.Key);
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            DateTime now = DateTime.Now;

            return new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_jwtSettings.ExpirationMinutes)),
                signingCredentials: signingCredentials);
        }

        /// <summary>
        /// Get claim value from the <see cref="ClaimTypes.Name"/> claim of the access token.
        /// </summary>
        private string GetNameClaim(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthHelper.GetSymmetricSecurityKey(_jwtSettings.Key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = false
            };

            ClaimsPrincipal claimPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (claimPrincipal == null)
            {
                throw new UnauthorizedAccessException(
                    $"Invalid token: Could not validate or get {nameof(ClaimsPrincipal)} from access token.");
            }

            Claim? nameClaim = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            if (nameClaim == null || string.IsNullOrEmpty(nameClaim.Value))
            {
                throw new UnauthorizedAccessException(
                    $"Invalid token: Could not get user name from access token.");
            }

            return nameClaim.Value;
        }

        #endregion
    }
}
