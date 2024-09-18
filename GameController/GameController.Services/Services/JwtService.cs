using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace GameController.Services.Services
{
    /// <summary>
    /// Provides functionality for generating JSON Web Tokens (JWT).
    /// </summary>
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Provides functionality for generating JSON Web Tokens (JWT).
        /// </summary>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// GetConfigurationKey.
        /// </summary>
        public string GetConfigurationKey()
        {
            var key = _configuration["Jwt:Key"];
            if (key == null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Configuration is not configured.");
            }

            return key;
        }

        /// <summary>
        /// Метод для получения ключа.
        /// </summary>
        public string GetKey()
        {
            var key = _configuration["Jwt:Key"];
            if (key == null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Configuration is not configured.");
            }

            return key;
        }

        /// <summary>
        /// Метод для получения издателя.
        /// </summary>
        public string GetIssuer()
        {
            var issuer = _configuration["Jwt:Issuer"];
            if (issuer == null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Configuration is not configured.");
            }

            return issuer;
        }

        /// <summary>
        /// Метод для получения аудитории.
        /// </summary>
        public string GetAudience()
        {
            var audience = _configuration["Jwt:Audience"];
            if (audience == null)
            {
                throw new ArgumentNullException(nameof(_configuration), "Configuration is not configured.");
            }

            return audience;
        }

        /// <summary>
        /// GenerateTokens.
        /// </summary>
        public (string Token, string RefreshToken) GenerateTokens(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username), "Username cannot be null or empty.");
            }

            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("JWT Key is not configured.");
            }

            // Читаем время жизни токена из конфигурации
            var expirationMinutesString = _configuration["Jwt:ExpirationMinutes"];
            if (string.IsNullOrEmpty(expirationMinutesString))
            {
                throw new ArgumentException("JWT ExpirationMinutes is not configured.");
            }

            var expirationMinutes = int.Parse(expirationMinutesString);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),  // Используем время из конфигурации
                signingCredentials: credentials);

            var refreshToken = GenerateRefreshToken();  // Генерация рефреш токена
            return (new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
        }

        /// <summary>
        /// GenerateRefreshToken.
        /// </summary>
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
