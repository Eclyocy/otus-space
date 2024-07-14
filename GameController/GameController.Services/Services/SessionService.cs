using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Exceptions;
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

        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        private readonly IGeneratorService _generatorService;
        private readonly IRabbitMQService _rabbitmqService;
        private readonly IShipService _shipService;

        private readonly ILogger<SessionService> _logger;

        private readonly IMapper _mapper;

        #endregion

        #region constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SessionService(
            ISessionRepository sessionRepository,
            IUserRepository userRepository,
            IGeneratorService generatorService,
            IRabbitMQService rabbitMQService,
            IShipService shipService,
            ILogger<SessionService> logger,
            IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;

            _generatorService = generatorService;
            _rabbitmqService = rabbitMQService;
            _shipService = shipService;

            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region public methods

        /// <inheritdoc/>
        public async Task<SessionDto> CreateUserSessionAsync(Guid userId)
        {
            _logger.LogInformation("Create session for user {userId}", userId);

            if (_userRepository.Get(userId) == null)
            {
                _logger.LogError("User {userId} not found.", userId);

                throw new NotFoundException($"User {userId} not found.");
            }

            CreateSessionDto createSessionDto = await CreateSessionRequestAsync();

            Session sessionRequest = _mapper.Map<Session>(createSessionDto);
            sessionRequest.UserId = userId;
            Session session = _sessionRepository.Create(sessionRequest);

            return _mapper.Map<SessionDto>(session);
        }

        /// <inheritdoc/>
        public List<SessionDto> GetUserSessions(Guid userId)
        {
            _logger.LogInformation("Get sessions of user {userId}", userId);

            List<Session> sessions = _sessionRepository.GetAllByUserId(userId);

            return _mapper.Map<List<SessionDto>>(sessions);
        }

        /// <inheritdoc/>
        public SessionDto GetUserSession(Guid userId, Guid sessionId)
        {
            _logger.LogInformation(
                "Get information on session {sessionId} of user {userId}",
                sessionId,
                userId);

            Session? session = _sessionRepository.Get(sessionId);

            if (session is null)
            {
                _logger.LogInformation("Session with {sessionId} is not found.", sessionId);

                throw new NotFoundException($"Session with {sessionId} is not found.");
            }

            if (session is not null && session.UserId != userId)
            {
                _logger.LogInformation(
                    "Session with {sessionId} is linked to {sessionUserId}, but was requested for {userId}.",
                    sessionId,
                    session.UserId,
                    userId);

                throw new ConflictException($"Session {sessionId} is linked to another user.");
            }

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

        #region private methods

        private async Task<CreateSessionDto> CreateSessionRequestAsync()
        {
            Task<Guid> shipTask = _shipService.CreateShipAsync();
            Task<Guid> generatorTask = _generatorService.CreateGeneratorAsync();

            await Task.WhenAll(shipTask, generatorTask);

            return new()
            {
                ShipId = shipTask.Result,
                GeneratorId = generatorTask.Result
            };
        }

        #endregion
    }
}
