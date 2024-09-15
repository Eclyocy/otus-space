using GameController.API.Models.User;
using GameController.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameController.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            if (login.Username == "test" && login.Password == "password")
            {
                var token = _jwtService.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
