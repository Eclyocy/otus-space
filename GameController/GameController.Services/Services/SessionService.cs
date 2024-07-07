using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Interfaces;
using GameController.Services.Models.Message;
using GameController.Services.Models.Session;
using Microsoft.Extensions.Logging;

namespace GameController.Services.Services
{
    /// <summary>
    /// Class for working with game sessions.
    /// </summary>
    public class SessionService : ISessionService
    {
        #region private fields

        private readonly IRabbitMQService _rabbitmqService;
        private readonly ISessionRepository _sessionRepository;
        private readonly ILogger<SessionService> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionService(
            IRabbitMQService rabbitMQService,
            ILogger<SessionService> logger,
            IMapper mapper,
            ISessionRepository sessionRepository)
        {
            _rabbitmqService = rabbitMQService;

            _logger = logger;
            _mapper = mapper;
            _sessionRepository = sessionRepository;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public SessionDto CreateUserSession(Guid userId, Guid shipId, Guid generatorId)
        {
            _logger.LogInformation(
                "Create session for user {userId} with ship {shipId} and generator {generatorId}",
                userId,
                shipId,
                generatorId);

            Session session = new()
            {
                UserId = userId,
                ShipId = shipId,
                GeneratorId = generatorId,
            };
            Session dbSession = _sessionRepository.Create(session);

            return _mapper.Map<SessionDto>(dbSession);
        }

        /// <inheritdoc/>
        public List<SessionDto> GetUserSessions(Guid userId)
        {
            _logger.LogInformation("Get sessions of user {userId}", userId);

            List<Session> sessions = _sessionRepository.GetAll(userId);

            return _mapper.Map<List<SessionDto>>(sessions);
        }

        /// <inheritdoc/>
        public SessionDto GetUserSession(Guid userId, Guid sessionId)
        {
            _logger.LogInformation(
                "Get information on session {sessionId}",
                sessionId);

            Session? session = _sessionRepository.Get(sessionId);

            return _mapper.Map<SessionDto>(session);
        }

        /// <inheritdoc/>
        public void MakeMove(Guid userId, Guid sessionId)
        {
            _logger.LogInformation(
                "Make move in session {sessionId} of user {userId}",
                sessionId,
                userId);

            SessionDto sessionDto = GetUserSession(userId, sessionId);

            NewDayMessage newDayMessage = _mapper.Map<NewDayMessage>(sessionDto);

            _rabbitmqService.SendNewDayMessage(newDayMessage);
        }

        #endregion
    }
}
