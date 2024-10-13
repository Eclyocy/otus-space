using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Models.Generator;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EventGenerator.Tests
{
    [TestFixture]
    public class GeneratorServiceTest
    {
        #region private fields

        private GeneratorService _generatorService;
        private EventService _eventService;
        private Mock<IGeneratorRepository> _generatorRepositoryMock;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IEventService> _eventServiceMock;
        private Mock<ILogger<GeneratorService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private readonly Guid shipId = Guid.NewGuid();
        private readonly Guid generatorId = Guid.NewGuid();
        private readonly Guid eventId = Guid.NewGuid();
        private readonly int troubleCoins = new Random().Next(0, 3);
        private readonly int eventLevelIndex = new Random().Next(3);

        #endregion

        #region setup
        [SetUp]
        public void SetupGeneratorServiceTests()
        {
            _generatorRepositoryMock = new Mock<IGeneratorRepository>();
            _eventServiceMock = new Mock<IEventService>();
            _loggerMock = new Mock<ILogger<GeneratorService>>();
            _mapperMock = new Mock<IMapper>();
            _generatorService = new GeneratorService(_generatorRepositoryMock.Object, _eventServiceMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        #endregion

        #region tests for CreateGenerator

        [Test]
        public void CreateGenerator_ReturnGeneratorDto_WhenGeneratorСorrect()
        {
            // Arrange
            var generator = new Generator { Id = generatorId, ShipId = shipId, TroubleCoins = troubleCoins };
            var createGeneratorDto = new CreateGeneratorDto { ShipId = shipId };
            var generatorDto = new GeneratorDto { GeneratorId = generatorId, ShipId = shipId, TroubleCoins = troubleCoins };
            _mapperMock.Setup(m => m.Map<Generator>(createGeneratorDto)).Returns(generator);
            _generatorRepositoryMock.Setup(repo => repo.Create(generator)).Returns(generator);
            _mapperMock.Setup(m => m.Map<GeneratorDto>(generator)).Returns(generatorDto);

            // Act
            var result = _generatorService.CreateGenerator(createGeneratorDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(generatorDto));
                Assert.That(result.GeneratorId, Is.EqualTo(generatorDto.GeneratorId));
                Assert.That(result.ShipId, Is.EqualTo(generatorDto.ShipId));
                Assert.That(result.TroubleCoins, Is.EqualTo(generatorDto.TroubleCoins));

                // Verify calls
                _generatorRepositoryMock.Verify(repo => repo.Create(generator), Times.Once);
                _mapperMock.Verify(m => m.Map<Generator>(createGeneratorDto), Times.Once);
                _mapperMock.Verify(m => m.Map<GeneratorDto>(generator), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for GetGenerator

        [Test]
        public void GetGenerator_ReturnsGeneratorDto_WhenGeneratorExists()
        {
            // Arrange
            var generator = new Generator { Id = generatorId, ShipId = shipId, TroubleCoins = troubleCoins };
            var generatorDto = new GeneratorDto { ShipId = shipId };
            _generatorRepositoryMock.Setup(repo => repo.Get(generatorId, false)).Returns(generator);
            _mapperMock.Setup(m => m.Map<GeneratorDto>(generator)).Returns(new GeneratorDto { GeneratorId = generatorId, ShipId = generator.ShipId, TroubleCoins = generator.TroubleCoins });

            // Act
            var result = _generatorService.GetGenerator(generatorId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.GeneratorId, Is.EqualTo(generatorDto.GeneratorId));
                Assert.That(result.ShipId, Is.EqualTo(generatorDto.ShipId));
                Assert.That(result.TroubleCoins, Is.EqualTo(generatorDto.TroubleCoins));

                // Verify calls
                _generatorRepositoryMock.Verify(repo => repo.Get(generatorId, false), Times.Once);

                _mapperMock.Verify(m => m.Map<GeneratorDto>(generator), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void GetGenerator_ThrowsNotFoundException_WhenGeneratorDoesNotExist()
        {
            // Arrange
            _generatorRepositoryMock.Setup(repo => repo.Get(generatorId, false)).Returns((Generator)null);

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotFoundException>(() => _generatorService.GetGenerator(generatorId));

                _generatorRepositoryMock.Verify(repo => repo.Get(generatorId, false), Times.Once);
                _generatorRepositoryMock.VerifyNoOtherCalls();

                _mapperMock.Verify(m => m.Map<GeneratorDto>(It.IsAny<Generator>()), Times.Never);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion

        /*
        #region AddTroubleCoin
        
        [Test]
        public void AddTroubleCoin_ReturnsGeneratorDto_WhenGeneratorExists()
        {
            // Arrange
            var generatorDto = new Generator { Id = generatorId, ShipId = shipId, TroubleCoins = troubleCoins };
            var newDayMessage = new NewDayMessageDto { GeneratorId = generatorId, ShipId = shipId };
            
            _generatorRepositoryMock.Setup(repo => repo.Get(generatorId, false)).Returns(generatorDto);
            _mapperMock.Setup(x => x.Map<Generator>(newDayMessage)).Returns(generatorDto);
            
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
        public void AddTroubleCoin_ThrowsNotModifiedException_WhenDataIsNotModified()
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
        */

        #region CreateEvent

        [Test]
        public void CreateEvent_ReturnEventDto_WhenGeneratorСorrect()
        {
            // Arrange
            var _event = new Event { Id = eventId, GeneratorId = generatorId, EventLevel = (EventLevel)Enum.GetValues(typeof(EventLevel)).GetValue(eventLevelIndex) }; 
            var _createEventDto = new CreateEventDto { GeneratorId = generatorId };
            var _eventDto = new EventDto { GeneratorId = generatorId, EventId = eventId, EventLevel = eventLevelIndex };
            _mapperMock.Setup(m => m.Map<Event>(_createEventDto)).Returns(_event);
            _eventRepositoryMock.Setup(repo => repo.Create(_event)).Returns(_event);
            _mapperMock.Setup(m => m.Map<EventDto>(_event)).Returns(_eventDto);

            // Act
            var result = _eventService.CreateEvent(_createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(_eventDto));
                Assert.That(result.GeneratorId, Is.EqualTo(_eventDto.GeneratorId));
                Assert.That(result.EventId, Is.EqualTo(_eventDto.EventId));
                Assert.That(result.EventLevel, Is.EqualTo(_eventDto.EventLevel));

                // Verify calls
                _eventRepositoryMock.Verify(repo => repo.Create(_event), Times.Once);
                _mapperMock.Verify(m => m.Map<Event>(_createEventDto), Times.Once);
                _mapperMock.Verify(m => m.Map<EventDto>(_event), Times.Once);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }
        #endregion

        #region GetGeneratorEvents

        [Test]
        public void GetGeneratorEvents_WhenGeneratorIdIsValid()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { Id = eventId, GeneratorId = generatorId }
            };

            EventDto eventDto = new EventDto { EventId = eventId, GeneratorId = generatorId };

            _eventRepositoryMock.Setup(repo => repo.GetAllByGeneratorId(generatorId)).Returns(events);
            _mapperMock
                .Setup(x => x.Map<List<EventDto>>(It.IsAny<List<Event>>()))
                .Returns(new List<EventDto>() { eventDto });

            // Act
            var actualEvents = _eventService.GetEvents(generatorId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualEvents, Is.Not.Null);
                Assert.That(actualEvents.Count, Is.EqualTo(events.Count));

                foreach (var item in actualEvents)
                {
                    Assert.That(item.EventId, Is.EqualTo(eventDto.EventId));
                    Assert.That(item.GeneratorId, Is.EqualTo(eventDto.GeneratorId));
                }

                _mapperMock.Verify(x => x.Map<List<EventDto>>(It.IsAny<List<Event>>()), Times.Once);
                _eventRepositoryMock.Verify(repo => repo.GetAllByGeneratorId(generatorId), Times.Once);
                _eventRepositoryMock.VerifyNoOtherCalls();
            });
        }

        [Test]
        public void GetGeneratorEvents_WhenGeneratorIdIsInvalid()
        {
            // Arrange
            _eventRepositoryMock.Setup(repo => repo.GetAllByGeneratorId(generatorId)).Returns(new List<Event>());
            _mapperMock.Setup(x => x.Map<List<EventDto>>(It.IsAny<List<Event>>())).Returns(new List<EventDto>());

            // Act
            var actualEvents = _eventService.GetEvents(generatorId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualEvents, Is.Not.Null);
                Assert.That(actualEvents.Count, Is.EqualTo(0));
                _mapperMock.Verify(x => x.Map<List<EventDto>>(It.IsAny<List<Event>>()), Times.Once);
                _eventRepositoryMock.Verify(repo => repo.GetAllByGeneratorId(generatorId), Times.Once);
                _eventRepositoryMock.VerifyNoOtherCalls();
            });
        }

        #endregion
    }
}
