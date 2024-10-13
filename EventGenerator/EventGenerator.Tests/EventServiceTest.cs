using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EventGenerator.Tests
{
    [TestFixture]
    public class EventServiceTest
    {
        #region private fields

        private EventService _eventService;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IEventService> _eventServiceMock;
        private Mock<ILogger<EventService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private readonly Guid generatorId = Guid.NewGuid();
        private readonly Guid eventId = Guid.NewGuid();
        private readonly int eventLevelIndex = new Random().Next(3);

        #endregion

        #region setup
        [SetUp]
        public void SetupGeneratorServiceTests()
        {
            _eventServiceMock = new Mock<IEventService>();
            _loggerMock = new Mock<ILogger<EventService>>();
            _mapperMock = new Mock<IMapper>();
            _eventService = new EventService(_eventRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }
        #endregion

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

    }
}

//public EventDto CreateEvent(CreateEventDto createEventRequest)
//public List<EventDto> GetEvents(Guid generatorId)
