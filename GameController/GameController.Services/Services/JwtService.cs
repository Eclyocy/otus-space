using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        /// GenerateToken.
        /// </summary>
        public string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT key is not configured.")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username), // Добавляем username как subject
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Добавляем уникальный идентификатор токена
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims, // Добавляем claims в токен
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
