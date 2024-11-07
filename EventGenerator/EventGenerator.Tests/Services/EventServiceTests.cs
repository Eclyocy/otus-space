using AutoMapper;
using EventGenerator.Database.Models;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Mappers;
using EventGenerator.Services.Models.Event;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Enums;

namespace EventGenerator.Tests.Services
{
    /// <summary>
    /// Tests for <see cref="EventService"/>.
    /// </summary>
    [TestFixture]
    public class EventServiceTests
    {
        #region private fields

        private readonly Guid _generatorId = Guid.NewGuid();
        private readonly Guid _eventId = Guid.NewGuid();

        private Mock<IEventBuilder> _eventBuidlerMock;
        private Mock<ILogger<EventService>> _loggerMock;

        private IMapper _mapper;

        private EventService _eventService;

        #endregion

        #region setup

        /// <summary>
        /// One-time setup.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _eventBuidlerMock = new Mock<IEventBuilder>();
            _loggerMock = new Mock<ILogger<EventService>>();

            _mapper = new Mapper(
                new MapperConfiguration(
                    static cfg =>
                    {
                        cfg.AddProfile<EventMapper>();
                    }));

            _eventService = new EventService(
                _eventBuidlerMock.Object,
                _loggerMock.Object,
                _mapper);
        }

        /// <summary>
        /// Setup for every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _eventBuidlerMock.Invocations.Clear();
            _loggerMock.Invocations.Clear();
        }

        #endregion

        #region tests for CreateEvent

        /// <summary>
        /// Test that <see cref="EventService.CreateEvent"/>
        /// creates an event based on the one provided by builder.
        /// </summary>
        [TestCase(EventLevel.Low)]
        [TestCase(EventLevel.Medium)]
        [TestCase(EventLevel.High)]
        public void Test_CreateEvent_WhenBuilt(EventLevel eventLevel)
        {
            // Arrange
            CreateEventDto createEventDto = new() { GeneratorId = _generatorId };
            Event builtEvent = new() { Id = _eventId, GeneratorId = _generatorId, EventLevel = eventLevel };

            _eventBuidlerMock.Setup(x => x.Build(It.IsAny<CreateEventDto>())).Returns(builtEvent);

            // Act
            EventDto? result = _eventService.CreateEvent(createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.GeneratorId, Is.EqualTo(_generatorId));
                Assert.That(result?.EventId, Is.EqualTo(_eventId));
                Assert.That(result?.EventLevel, Is.EqualTo(eventLevel));

                _eventBuidlerMock.Verify(x => x.Build(createEventDto), Times.Once);
                _eventBuidlerMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        /// <summary>
        /// Test that <see cref="EventService.CreateEvent"/>
        /// does not create an event if the builder does not provide it.
        /// </summary>
        [Test]
        public void Test_CreateEvent_WhenNull()
        {
            // Arrange
            CreateEventDto createEventDto = new() { GeneratorId = _generatorId };

            _eventBuidlerMock.Setup(x => x.Build(It.IsAny<CreateEventDto>())).Returns((Event?)null);

            // Act
            EventDto? result = _eventService.CreateEvent(createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Null);

                _eventBuidlerMock.Verify(x => x.Build(createEventDto), Times.Once);
                _eventBuidlerMock.VerifyNoOtherCalls();

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion
    }
}
