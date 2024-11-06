using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Enums;

namespace EventGenerator.Tests
{
    [TestFixture]
    public class EventServiceTest
    {
        #region private fields

        private EventService _eventService;
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<ILogger<EventService>> _loggerMock;
        private Mock<IMapper> _mapperMock;
        private readonly Guid _generatorId = Guid.NewGuid();
        private readonly Guid _eventId = Guid.NewGuid();
        private readonly int _eventLevelIndex = new Random().Next(1,3);

        #endregion

        #region setup
        [SetUp]
        public void SetupGeneratorServiceTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
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
            var _event = new Event { Id = _eventId, GeneratorId = _generatorId, EventLevel = (EventLevel)_eventLevelIndex };
            var _createEventDto = new CreateEventDto { GeneratorId = _generatorId };
            var _eventDto = new EventDto { GeneratorId = _generatorId, EventId = _eventId, EventLevel = (EventLevel)_eventLevelIndex };
            _mapperMock.Setup(m => m.Map<Event>(_createEventDto)).Returns(_event);
            _eventRepositoryMock.Setup(repo => repo.Create(_event)).Returns(_event);
            _mapperMock.Setup(m => m.Map<EventDto>(_event)).Returns(_eventDto);

            // Act
            var result = _eventService.CreateEvent(_createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                //Assert.That(result, Is.Not.Null);
                //Assert.That(result, Is.EqualTo(_eventDto));
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
    }
}
