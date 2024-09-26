using Microsoft.AspNetCore.Mvc;

namespace GameController.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        [HttpGet("play")]
        public IActionResult Play()
        {
            var username = User.Identity.Name; // Извлечение информации о пользователе из токена
            return Ok($"Welcome {username}, you are authorized to play!");
        }
    }
}
