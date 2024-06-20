using GameController.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionController.Controllers
{
    /// <summary>
    /// Controller for actions with user's game sessions (create, get).
    /// </summary>
    [ApiController]
    [Route("/api/users/{userId}/sessions")]
    public class SessionController : ControllerBase
    {
        #region private fields

        private readonly ISessionService _sessionService;

        #endregion

        #region constructor

        public SessionController(
            ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #endregion

        #region public methods

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание пользовательской игровой сессии")]
        public void CreateUserSession(Guid userId)
        {
            _sessionService.CreateUserSession(userId);
        }

        #endregion
    }
}
