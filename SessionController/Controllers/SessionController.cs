using Microsoft.AspNetCore.Mvc;
using SessionController.Services;

namespace SessionController.Controllers
{
    /// <summary>
    /// Контроллер для действий с игровыми сессиями (создание, получение).
    /// </summary>
    [ApiController]
    [Route("/api/users/{userId:guid}")]
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
        public void CreateUserSession(Guid userId)
        {
            _sessionService.CreateUserSession(userId);
        }

        #endregion
    }
}
