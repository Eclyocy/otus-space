

using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Exceptions;
using GameController.Services.Interfaces;
using GameController.Services.Models.Message;
using GameController.Services.Models.Session;
using GameController.Services.Models.Ship;
using GameController.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameController.Services.Tests
{
    [TestFixture]
    internal class SessionServiceTests
    {

        #region private fields

        private Mock<ISessionRepository> _sessionRepositoryMock;
        private Mock<ISessionService> _sessionServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IGeneratorService> _generatorServiceMock;
        private Mock<IRabbitMQService> _rabbitMQServiceMock;
        private Mock<IShipService> _shipServiceMock;
        private Mock<ILogger<SessionService>> _loggerMock;
        private Mock<IMapper> _mapperMock;

        private SessionService _sessionService;

        private readonly Guid userId = Guid.NewGuid();
        private readonly Guid sessionId = Guid.NewGuid();
        private const string Name = "Test User";
        private const string PasswordHash = "Test User";

        #endregion

        #region setup

        [SetUp]
        public void SetUp()
        {
            _sessionRepositoryMock = new Mock<ISessionRepository>();
            _sessionServiceMock = new Mock<ISessionService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _generatorServiceMock = new Mock<IGeneratorService>();
            _rabbitMQServiceMock = new Mock<IRabbitMQService>();
            _shipServiceMock = new Mock<IShipService>();
            _loggerMock = new Mock<ILogger<SessionService>>();
            _mapperMock = new Mock<IMapper>();

            _sessionService = new SessionService(
                _sessionRepositoryMock.Object,
                _userRepositoryMock.Object,
                _generatorServiceMock.Object,
                _rabbitMQServiceMock.Object,
                _shipServiceMock.Object,
                _loggerMock.Object,
                _mapperMock.Object);
        }

        #endregion

        #region CreateUserSessionAsync

        [Test]
        public async Task CreateUserSessionAsync_WhenUiserIDExists()
        {
            // Arrange
            SessionDto sessionDto = new SessionDto { SessionId = Guid.NewGuid() };

            _userRepositoryMock.Setup(x => x.Get(userId)).Returns(new User { Id = userId, Name = Name, PasswordHash = PasswordHash });

            _sessionRepositoryMock.Setup(x => x.Create(It.IsAny<Session>())).Returns(new Session());

            _mapperMock.Setup(x => x.Map<Session>(It.IsAny<CreateSessionDto>())).Returns(new Session());
            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(sessionDto);

            // Act
            SessionDto actualSessionDto = await _sessionService.CreateUserSessionAsync(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual(sessionDto, actualSessionDto);
                _sessionRepositoryMock.Verify(x => x.Create(It.IsAny<Session>()), Times.Once);

                _mapperMock.Verify(x => x.Map<Session>(It.IsAny<CreateSessionDto>()), Times.Once);
                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Once);

                _sessionRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public async Task CreateUserSessionAsync_WhenUserIdIsNull()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.Get(userId)).Returns((Session)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(() => _sessionService.CreateUserSessionAsync(userId));
            Assert.Multiple(() =>
            {  
                Assert.That(exception.Message, Is.EqualTo($"User {userId} not found."));
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion

        #region GetUserSessions

        [Test]
        public void GetUserSessions_WhenUserIdIsValid()
        {
            // Arrange
            var sessions = new List<Session>
            {
                new Session { Id = sessionId, UserId = userId }
            };

            SessionDto sessionDto = new SessionDto { SessionId = sessionId, UserId = userId };

            _sessionRepositoryMock.Setup(repo => repo.GetAllByUserId(userId)).Returns(sessions);
            _mapperMock.Setup(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>())).Returns(new List<SessionDto>() { sessionDto });

            // Act
            var actualSessions = _sessionService.GetUserSessions(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualSessions, Is.Not.Null);
                Assert.That(actualSessions.Count, Is.EqualTo(sessions.Count));

                foreach (var item in actualSessions)
                {
                    Assert.That(item.SessionId, Is.EqualTo(sessionDto.SessionId));
                    Assert.That(item.UserId, Is.EqualTo(sessionDto.UserId));
                }

                _sessionRepositoryMock.Verify(repo => repo.GetAllByUserId(userId), Times.Once);
            });
        }

        [Test]
        public void GetUserSessions_WhenUserIdIsInvalid()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.GetAllByUserId(userId)).Returns(new List<Session>());
            _mapperMock.Setup(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>())).Returns(new List<SessionDto>());

            // Act
            var actualSessions = _sessionService.GetUserSessions(userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualSessions, Is.Not.Null);
                Assert.That(actualSessions.Count, Is.EqualTo(0));

                _sessionRepositoryMock.Verify(repo => repo.GetAllByUserId(userId), Times.Once);
            });
        }

        #endregion

        #region GetUserSession

        [Test]
        public void GetUserSession_ValidUserId()
        {
            // Arrange
            Session? session = new Session { Id = sessionId, UserId = userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() };

            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).Returns(new Session { Id = sessionId, UserId = userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() });
            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(new SessionDto { SessionId = sessionId, UserId = userId });

            // Act
            var result = _sessionService.GetUserSession(userId, sessionId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.SessionId, Is.EqualTo(session.Id));

                _sessionRepositoryMock.Verify(repo => repo.Get(sessionId), Times.Once);
                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void GetUserSession_WhenSessionIsNull()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).Returns((Session)null);

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() => _sessionService.GetUserSession(userId, sessionId));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {sessionId} is not found."));

                _sessionRepositoryMock.Verify(repo => repo.Get(sessionId), Times.Once);
                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Never);
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void GetUserSession_WhenSessionUserIdDoesNotMatch()
        {
            // Arrange

            Guid differentUserId = Guid.NewGuid();

            var session = new Session { Id = sessionId, UserId = differentUserId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() };
            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).Returns(session);

            // Act & Assert
            var exception = Assert.Throws<ConflictException>(() => _sessionService.GetUserSession(userId, sessionId));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session {sessionId} is linked to another user."));
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));

                _sessionRepositoryMock.Verify(repo => repo.Get(sessionId), Times.Once);
                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Never);
            });
        }

        #endregion

        #region GetUserSessionShipAsync

        [Test]
        public async Task GetUserSessionShipAsync_WhenSessionExists_ReturnsShipDto()
        {
            // Arrange
            var shipId = Guid.NewGuid();
            var sessionDto = new SessionDto { SessionId = sessionId, ShipId = shipId };
            var shipDto = new ShipDto { Id = shipId };

            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).
                Returns(new Session { Id = sessionId, UserId = userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() });
            _shipServiceMock.Setup(x => x.GetShipAsync(shipId)).ReturnsAsync(shipDto);

            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(sessionDto);
            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(sessionDto);


            // Act
            var result = await _sessionService.GetUserSessionShipAsync(userId, sessionId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(shipId, result.Id);
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });

            _shipServiceMock.Verify(x => x.GetShipAsync(shipId), Times.Once);
        }

        [Test]
        public void GetUserSessionShipAsync_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionServiceMock.Setup(x => x.GetUserSession(userId, sessionId))
                .Throws(new NotFoundException($"Session with {sessionId} is not found."));

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
                await _sessionService.GetUserSessionShipAsync(userId, sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {sessionId} is not found."));
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(3));

                _shipServiceMock.Verify(x => x.GetShipAsync(It.IsAny<Guid>()), Times.Never);
            });
        }

        #endregion

        #region DeleteUserSession

        [Test]
        public void DeleteUserSession_WhenSessionExists_CallsDeleteOnRepository()
        {
            // Arrange
            var sessionDto = new SessionDto { SessionId = sessionId, ShipId = Guid.NewGuid() };
            var session = new Session { Id = sessionId, UserId = userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() };

            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).Returns(session);
            _sessionRepositoryMock.Setup(x => x.Delete(sessionId)).Verifiable();

            _mapperMock.Setup(x => x.Map<SessionDto>(session)).Returns(sessionDto);

            // Act
            _sessionService.DeleteUserSession(userId, sessionId);

            // Assert
            _sessionRepositoryMock.Verify(x => x.Delete(sessionId), Times.Once);
            Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
        }

        [Test]
        public void DeleteUserSession_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionServiceMock.Setup(x => x.GetUserSession(userId, sessionId))
                .Throws(new NotFoundException($"Session with {sessionId} is not found."));

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() =>
                _sessionService.DeleteUserSession(userId, sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {sessionId} is not found."));
                _sessionRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(3));
            });
        }

        #endregion

        #region DeleteUserSession
        [Test]
        public void MakeMove_WhenCalled_CorrectlyProcessesMove()
        {
            // Arrange
            var shipId = Guid.NewGuid();
            var sessionDto = new SessionDto { SessionId = sessionId, ShipId = shipId };
            var newDayMessage = new NewDayMessage { ShipId = shipId };
            var session = new Session { Id = sessionId, UserId = userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() };

            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId)).Returns(session);

            _mapperMock.Setup(x => x.Map<SessionDto>(session)).Returns(sessionDto);
            _mapperMock.Setup(x => x.Map<NewDayMessage>(sessionDto)).Returns(newDayMessage);

            _rabbitMQServiceMock.Setup(x => x.SendNewDayMessage(newDayMessage)).Verifiable();

            // Act
            _sessionService.MakeMove(userId, sessionId);

            // Assert
            _mapperMock.Verify(x => x.Map<NewDayMessage>(sessionDto), Times.Once);
            _rabbitMQServiceMock.Verify(x => x.SendNewDayMessage(newDayMessage), Times.Once);

            Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
        }

        [Test]
        public void MakeMove_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.Get(sessionId))
                .Throws(new NotFoundException($"Session with {sessionId} is not found."));

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() =>
                _sessionService.MakeMove(userId, sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {sessionId} is not found."));

                _mapperMock.Verify(x => x.Map<NewDayMessage>(It.IsAny<SessionDto>()), Times.Never);
                _rabbitMQServiceMock.Verify(x => x.SendNewDayMessage(It.IsAny<NewDayMessage>()), Times.Never);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion

    }
}
