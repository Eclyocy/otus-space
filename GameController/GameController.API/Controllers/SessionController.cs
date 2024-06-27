using AutoMapper;
using GameController.API.Models.Session;
using GameController.Services.Interfaces;
using GameController.Services.Models.Session;
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
        private readonly IShipService _shipService;
        private readonly IGeneratorService _generatorService;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionController(
            ISessionService sessionService,
            IShipService shipService,
            IGeneratorService generatorService,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _shipService = shipService;
            _generatorService = generatorService;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        [HttpGet]
        [Route("")]
        [SwaggerOperation("Получение информации обо всех пользовательских игровых сессиях")]
        public List<SessionModel> GetUserSessions(Guid userId)
        {
            List<SessionDto> sessionDtos = _sessionService.GetUserSessions(userId);
            List<SessionModel> sessionModels = _mapper.Map<List<SessionModel>>(sessionDtos);
            return sessionModels;
        }

        [HttpPost]
        [Route("")]
        [SwaggerOperation("Создание пользовательской игровой сессии")]
        public async Task<SessionModel> CreateUserSessionAsync(Guid userId)
        {
            (Guid shipId, Guid generatorId) = await CreateExternalObjectsAsync();

            SessionDto sessionDto = _sessionService.CreateUserSession(userId, shipId, generatorId);

            return _mapper.Map<SessionModel>(sessionDto);
        }

        [HttpGet]
        [Route("{sessionId}")]
        [SwaggerOperation("Получение информации о пользовательской игровой сессии")]
        public SessionModel CreateUserSession(Guid userId, Guid sessionId)
        {
            SessionDto sessionDto = _sessionService.GetUserSession(userId, sessionId);
            return _mapper.Map<SessionModel>(sessionDto);
        }

        #endregion

        #region private methods

        private async Task<(Guid shipId, Guid generatorId)> CreateExternalObjectsAsync()
        {
            Task<Guid> shipTask = _shipService.CreateShipAsync();
            Task<Guid> generatorTask = _generatorService.CreateGeneratorAsync();

            await Task.WhenAll(shipTask, generatorTask);

            return (shipTask.Result, generatorTask.Result);
        }

        #endregion
    }
}
