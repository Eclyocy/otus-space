using AutoMapper;
using GameController.API.Models.Session;
using GameController.API.Models.Ship;
using GameController.Services.Interfaces;
using GameController.Services.Models.Session;
using GameController.Services.Models.Ship;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameController.API.Controllers
{
    /// <summary>
    /// Controller for actions with user's game sessions.
    /// </summary>
    [ApiController]
    [Route("/api/users/{userId}/sessions")]
    public class SessionController : ControllerBase
    {
        #region private fields

        private readonly ISessionService _sessionService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionController(
            ISessionService sessionService,
            IMapper mapper)
        {
            _sessionService = sessionService;

            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Get all user sessions.
        /// </summary>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Получение списка всех игровых сессий пользователя")]
        public List<SessionResponse> GetUserSessions(Guid userId)
        {
            List<SessionDto> sessionDtos = _sessionService.GetUserSessions(userId);
            return _mapper.Map<List<SessionResponse>>(sessionDtos);
        }

        /// <summary>
        /// Create a new user session.
        /// </summary>
        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание пользовательской игровой сессии")]
        public async Task<SessionResponse> CreateUserSessionAsync(Guid userId)
        {
            SessionDto sessionDto = await _sessionService.CreateUserSessionAsync(userId);
            return _mapper.Map<SessionResponse>(sessionDto);
        }

        /// <summary>
        /// Get the user session.
        /// </summary>
        [HttpGet]
        [Route("{sessionId}")]
        [SwaggerOperation("Получение информации о пользовательской игровой сессии")]
        public SessionResponse GetUserSession(Guid userId, Guid sessionId)
        {
            SessionDto sessionDto = _sessionService.GetUserSession(userId, sessionId);
            return _mapper.Map<SessionResponse>(sessionDto);
        }

        /// <summary>
        /// Get the space ship of a particular user session.
        /// </summary>
        [HttpGet]
        [Route("{sessionId}/ship")]
        [SwaggerOperation("Получение информации о состоянии космического корабля в пользовательской игровой сессии")]
        public async Task<ShipResponse> GetUserSessionShipAsync(Guid userId, Guid sessionId)
        {
            ShipDto shipDto = await _sessionService.GetUserSessionShipAsync(userId, sessionId);
            return _mapper.Map<ShipResponse>(shipDto);
        }

        /// <summary>
        /// Send the "new move" command to the user session.
        /// </summary>
        [HttpPost]
        [Route("{sessionId}/make-move")]
        [SwaggerOperation("Сделать следующий ход в пользовательской игровой сессии")]
        public void MakeMove(Guid userId, Guid sessionId)
        {
            _sessionService.MakeMove(userId, sessionId);
        }

        /// <summary>
        /// Delete a particular user session.
        /// </summary>
        [HttpDelete]
        [Route("{sessionId}")]
        [SwaggerOperation("Удалить пользовательскую игровую сессию")]
        public void DeleteUserSession(Guid userId, Guid sessionId)
        {
            _sessionService.DeleteUserSession(userId, sessionId);
        }

        #endregion
    }
}
