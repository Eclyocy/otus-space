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
    public class SessionServiceTests
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

        private readonly Guid _userId = Guid.NewGuid();
        private readonly Guid _sessionId = Guid.NewGuid();
        private const string UserName = "Test User";
        private const string UserPasswordHash = "Test Password";

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
        public async Task CreateUserSessionAsync_WhenUserExists()
        {
            // Arrange
            _userRepositoryMock
                .Setup(x => x.Get(_userId))
                .Returns(new User() { Id = _userId, Name = UserName, PasswordHash = UserPasswordHash });

            Guid shipGuid = Guid.NewGuid();
            _shipServiceMock
                .Setup(x => x.CreateShipAsync())
                .ReturnsAsync(shipGuid);

            Guid generatorGuid = Guid.NewGuid();
            _generatorServiceMock
                .Setup(x => x.CreateGeneratorAsync())
                .ReturnsAsync(generatorGuid);

            _sessionRepositoryMock
                .Setup(x => x.Create(It.IsAny<Session>()))
                .Returns(new Session());

            _mapperMock
                .Setup(x => x.Map<Session>(
                    It.Is<CreateSessionDto>(x => x.ShipId == shipGuid && x.GeneratorId == generatorGuid )))
                .Returns(new Session() { ShipId = shipGuid, GeneratorId = generatorGuid });

            _mapperMock
                .Setup(x => x.Map<SessionDto>(
                    It.IsAny<Session>()))
                .Returns(new SessionDto());

            // Act
            SessionDto sessionDto = await _sessionService.CreateUserSessionAsync(_userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(sessionDto, Is.Not.Null);

                _userRepositoryMock.Verify(x => x.Get(_userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                _sessionRepositoryMock.Verify(x => x.Create(
                    It.Is<Session>(session =>
                        session.Id == Guid.Empty &&
                        session.UserId == _userId &&
                        session.ShipId == shipGuid &&
                        session.GeneratorId == generatorGuid)));
                _sessionRepositoryMock.VerifyNoOtherCalls();

                _generatorServiceMock.Verify(x => x.CreateGeneratorAsync(), Times.Once);
                _generatorServiceMock.VerifyNoOtherCalls();

                _shipServiceMock.Verify(x => x.CreateShipAsync(), Times.Once);
                _shipServiceMock.VerifyNoOtherCalls();

                _rabbitMQServiceMock.VerifyNoOtherCalls();

                _mapperMock.Verify(x => x.Map<Session>(It.IsAny<CreateSessionDto>()), Times.Once);
                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Once);
                _mapperMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void CreateUserSessionAsync_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.Get(_userId)).Returns((User?)null);

            // Act & Assert
            var exception = Assert
                .ThrowsAsync<NotFoundException>(() => _sessionService.CreateUserSessionAsync(_userId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"User {_userId} not found."));

                _userRepositoryMock.Verify(repo => repo.Get(_userId), Times.Once);
                _userRepositoryMock.VerifyNoOtherCalls();

                _sessionRepositoryMock.VerifyNoOtherCalls();

                _generatorServiceMock.VerifyNoOtherCalls();
                _shipServiceMock.VerifyNoOtherCalls();
                _rabbitMQServiceMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
                _mapperMock.VerifyNoOtherCalls();
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
                new Session { Id = _sessionId, UserId = _userId }
            };

            SessionDto sessionDto = new SessionDto { SessionId = _sessionId, UserId = _userId };

            _sessionRepositoryMock.Setup(repo => repo.GetAllByUserId(_userId)).Returns(sessions);
            _mapperMock
                .Setup(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>()))
                .Returns(new List<SessionDto>() { sessionDto });

            // Act
            var actualSessions = _sessionService.GetUserSessions(_userId);

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
                _mapperMock.Verify(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>()), Times.Once);
                _sessionRepositoryMock.Verify(repo => repo.GetAllByUserId(_userId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();
            });
        }

        [Test]
        public void GetUserSessions_WhenUserIdIsInvalid()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.GetAllByUserId(_userId)).Returns(new List<Session>());
            _mapperMock.Setup(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>())).Returns(new List<SessionDto>());

            // Act
            var actualSessions = _sessionService.GetUserSessions(_userId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualSessions, Is.Not.Null);
                Assert.That(actualSessions.Count, Is.EqualTo(0));
                _mapperMock.Verify(x => x.Map<List<SessionDto>>(It.IsAny<List<Session>>()), Times.Once);
                _sessionRepositoryMock.Verify(repo => repo.GetAllByUserId(_userId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();
            });
        }

        #endregion

        #region GetUserSession

        [Test]
        public void GetUserSession_ValidUserId()
        {
            // Arrange
            Session session = new Session {
                Id = _sessionId,
                UserId = _userId,
                GeneratorId = Guid.NewGuid(),
                ShipId = Guid.NewGuid()
            };

            _sessionRepositoryMock
                .Setup(repo => repo.Get(_sessionId))
                .Returns(new Session { Id = _sessionId, UserId = _userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() });
            _mapperMock
                .Setup(x => x.Map<SessionDto>(It.IsAny<Session>()))
                .Returns(new SessionDto { SessionId = _sessionId, UserId = _userId });

            // Act
            var result = _sessionService.GetUserSession(_userId, _sessionId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.SessionId, Is.EqualTo(session.Id));

                _sessionRepositoryMock.Verify(repo => repo.Get(_sessionId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Once);
                _mapperMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void GetUserSession_WhenSessionIsNull()
        {
            // Arrange
            _sessionRepositoryMock.Setup(repo => repo.Get(_sessionId)).Returns((Session)null);

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() => _sessionService.GetUserSession(_userId, _sessionId));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {_sessionId} is not found."));

                _sessionRepositoryMock.Verify(repo => repo.Get(_sessionId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Never);
                _mapperMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void GetUserSession_WhenSessionUserIdDoesNotMatch()
        {
            // Arrange
            Guid differentUserId = Guid.NewGuid();

            var session = new Session { Id = _sessionId, UserId = differentUserId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() };
            _sessionRepositoryMock.Setup(repo => repo.Get(_sessionId)).Returns(session);

            // Act & Assert
            var exception = Assert.Throws<ConflictException>(() => _sessionService.GetUserSession(_userId, _sessionId));
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session {_sessionId} is linked to another user."));
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));

                _sessionRepositoryMock.Verify(repo => repo.Get(_sessionId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(x => x.Map<SessionDto>(It.IsAny<Session>()), Times.Never);
                _mapperMock.VerifyNoOtherCalls();
            });
        }

        #endregion

        #region GetUserSessionShipAsync

        [Test]
        public async Task GetUserSessionShipAsync_WhenSessionExists_ReturnsShipDto()
        {
            // Arrange
            var shipId = Guid.NewGuid();
            var sessionDto = new SessionDto { SessionId = _sessionId, ShipId = shipId };
            var shipDto = new ShipDto { Id = shipId };

            _sessionRepositoryMock.Setup(repo => repo.Get(_sessionId)).
                Returns(new Session { Id = _sessionId, UserId = _userId, GeneratorId = Guid.NewGuid(), ShipId = Guid.NewGuid() });
            _shipServiceMock.Setup(x => x.GetShipAsync(shipId)).ReturnsAsync(shipDto);

            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(sessionDto);
            _mapperMock.Setup(x => x.Map<SessionDto>(It.IsAny<Session>())).Returns(sessionDto);

            // Act
            var result = await _sessionService.GetUserSessionShipAsync(_userId, _sessionId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Id, Is.EqualTo(shipId));
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));

                _shipServiceMock.Verify(x => x.GetShipAsync(shipId), Times.Once);
                _shipServiceMock.VerifyNoOtherCalls();
            });
        }

        [Test]
        public void GetUserSessionShipAsync_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionServiceMock.Setup(x => x.GetUserSession(_userId, _sessionId))
                .Throws(new NotFoundException($"Session with {_sessionId} is not found."));

            // Act & Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () =>
                await _sessionService.GetUserSessionShipAsync(_userId, _sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {_sessionId} is not found."));
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
            var sessionDto = new SessionDto { SessionId = _sessionId, ShipId = Guid.NewGuid() };
            var session = new Session {
                Id = _sessionId,
                UserId = _userId,
                GeneratorId = Guid.NewGuid(),
                ShipId = Guid.NewGuid()
            };

            _sessionRepositoryMock.Setup(repo => repo.Get(_sessionId)).Returns(session);
            _sessionRepositoryMock.Setup(x => x.Delete(_sessionId)).Verifiable();

            _mapperMock.Setup(x => x.Map<SessionDto>(session)).Returns(sessionDto);

            // Act
            _sessionService.DeleteUserSession(_userId, _sessionId);

            // Assert
            Assert.Multiple(() =>
            {
                _sessionRepositoryMock.Verify(x => x.Delete(_sessionId), Times.Once);
                _sessionRepositoryMock.Verify(x => x.Get(_sessionId), Times.Once);
                _sessionRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public void DeleteUserSession_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionServiceMock.Setup(x => x.GetUserSession(_userId, _sessionId))
                .Throws(new NotFoundException($"Session with {_sessionId} is not found."));

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() =>
                _sessionService.DeleteUserSession(_userId, _sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {_sessionId} is not found."));
                _sessionRepositoryMock.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Never);
                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(3));
            });
        }

        #endregion

        #region MakeMove

        [Test]
        public void MakeMove_WhenCalled_CorrectlyProcessesMove()
        {
            // Arrange
            var shipId = Guid.NewGuid();
            var sessionDto = new SessionDto { SessionId = _sessionId, ShipId = shipId };
            var newDayMessage = new NewDayMessage { ShipId = shipId };
            var session = new Session {
                Id = _sessionId,
                UserId = _userId,
                GeneratorId = Guid.NewGuid(),
                ShipId = Guid.NewGuid()
            };

            _sessionRepositoryMock.Setup(repo => repo.Get(_sessionId)).Returns(session);

            _mapperMock.Setup(x => x.Map<SessionDto>(session)).Returns(sessionDto);
            _mapperMock.Setup(x => x.Map<NewDayMessage>(sessionDto)).Returns(newDayMessage);

            _rabbitMQServiceMock.Setup(x => x.SendNewDayMessage(newDayMessage)).Verifiable();

            // Act
            _sessionService.MakeMove(_userId, _sessionId);

            // Assert
            _mapperMock.Verify(x => x.Map<NewDayMessage>(sessionDto), Times.Once);
            _mapperMock.Verify(x => x.Map<SessionDto>(session), Times.Once);
            _mapperMock.VerifyNoOtherCalls();

            _rabbitMQServiceMock.Verify(x => x.SendNewDayMessage(newDayMessage), Times.Once);
            _rabbitMQServiceMock.VerifyNoOtherCalls();

            Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
        }

        [Test]
        public void MakeMove_WhenSessionDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _sessionRepositoryMock
                .Setup(repo => repo.Get(_sessionId))
                .Throws(new NotFoundException($"Session with {_sessionId} is not found."));

            // Act & Assert
            var exception = Assert.Throws<NotFoundException>(() =>
                _sessionService.MakeMove(_userId, _sessionId));

            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Is.EqualTo($"Session with {_sessionId} is not found."));

                _mapperMock.Verify(x => x.Map<NewDayMessage>(It.IsAny<SessionDto>()), Times.Never);
                _mapperMock.VerifyNoOtherCalls();

                _rabbitMQServiceMock.Verify(x => x.SendNewDayMessage(It.IsAny<NewDayMessage>()), Times.Never);
                _rabbitMQServiceMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion
    }
}
