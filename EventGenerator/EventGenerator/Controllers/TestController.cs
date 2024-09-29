using EventGenerator.API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EventGenerator.API.Controllers
{
    /// <summary>
    /// Controller for test actions.
    /// </summary>
    [Obsolete("Тестовый контроллер")]
    [ApiController]
    [Route("/api/test")]
    public class TestController : Controller
    {
        /// <summary>
        /// Generate guid.
        /// </summary>
        [HttpGet]
        [SwaggerOperation("Получение Guid корабля")]
        public CreateGeneratorRequest GetShip()
        {
            return new CreateGeneratorRequest()
            {
                ShipId = Guid.NewGuid()
            };
        }
    }
}
