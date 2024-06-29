using AutoMapper;
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
            IMapper mapper)
        {
            _rabbitmqService = rabbitMQService;

            _logger = logger;
            _mapper = mapper;
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

            return new()
            {
                UserId = userId,
                SessionId = Guid.NewGuid(),
                ShipId = shipId,
                GeneratorId = generatorId
            };
        }

        /// <inheritdoc/>
        public List<SessionDto> GetUserSessions(Guid userId)
        {
            _logger.LogInformation("Get sessions of user {userId}", userId);

            return new List<SessionDto>()
            {
                new()
                {
                    UserId = userId,
                    SessionId = Guid.NewGuid(),
                    ShipId = Guid.NewGuid(),
                    GeneratorId = Guid.NewGuid()
                },
                new()
                {
                    UserId = userId,
                    SessionId = Guid.NewGuid(),
                    ShipId = Guid.NewGuid(),
                    GeneratorId = Guid.NewGuid()
                }
            };
        }

        /// <inheritdoc/>
        public SessionDto GetUserSession(Guid userId, Guid sessionId)
        {
            _logger.LogInformation(
                "Get information on session {sessionId} of user {userId}",
                sessionId,
                userId);

            return new()
            {
                UserId = userId,
                SessionId = sessionId,
                ShipId = Guid.NewGuid(),
                GeneratorId = Guid.NewGuid()
            };
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
