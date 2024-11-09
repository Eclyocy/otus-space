using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Builders;
using EventGenerator.Services.Models.Event;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Enums;

namespace EventGenerator.Tests.Builders
{
    /// <summary>
    /// Tests for <see cref="EventBuilder"/>.
    /// </summary>
    [TestFixture]
    public class EventBuilderTests
    {
        #region private fields

        private readonly Guid _eventId = Guid.NewGuid();
        private readonly Guid _generatorId = Guid.NewGuid();

        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<ILogger<EventBuilder>> _loggerMock;
        private Mock<Random> _randomMock;

        private EventBuilder _eventBuilder;

        #endregion

        #region setup

        /// <summary>
        /// One-time setup.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _loggerMock = new Mock<ILogger<EventBuilder>>();
            _randomMock = new Mock<Random>();

            _eventRepositoryMock
                .Setup(x => x.Create(It.IsAny<Event>()))
                .Returns((Event e) => new Event
                {
                    Id = _eventId,
                    GeneratorId = e.GeneratorId,
                    EventLevel = e.EventLevel
                });

            _eventBuilder = new EventBuilder(
                _eventRepositoryMock.Object,
                _loggerMock.Object,
                _randomMock.Object);
        }

        /// <summary>
        /// Setup for every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _eventRepositoryMock.Invocations.Clear();
            _loggerMock.Invocations.Clear();
            _randomMock.Invocations.Clear();
        }

        #endregion

        #region tests for Build

        /// <summary>
        /// Test that <see cref="EventBuilder.Build"/> returns null
        /// when no trouble coins are supplied.
        /// </summary>
        [Test]
        public void Test_Build_WhenNoTroubleCoins()
        {
            // Arrange
            CreateEventDto createEventDto = new() { GeneratorId = _generatorId, TroubleCoins = 0 };

            // Act
            Event? result = _eventBuilder.Build(createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Null);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));

                _eventRepositoryMock.VerifyNoOtherCalls();
                _randomMock.VerifyNoOtherCalls();
            });
        }

        /// <summary>
        /// Test that <see cref="EventBuilder.Build"/> returns null
        /// when some trouble coins are supplied,
        /// but random generates zero.
        /// </summary>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Test_Build_WhenRandomGeneratesZero(int troubleCoins)
        {
            // Arrange
            CreateEventDto createEventDto = new() { GeneratorId = _generatorId, TroubleCoins = troubleCoins };

            _randomMock.Setup(x => x.Next(0, It.IsAny<int>())).Returns(0);

            // Act
            Event? result = _eventBuilder.Build(createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Null);

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(2));

                _eventRepositoryMock.VerifyNoOtherCalls();

                _randomMock.Verify(x => x.Next(0, troubleCoins + 1));
                _randomMock.VerifyNoOtherCalls();
            });
        }

        /// <summary>
        /// Test that <see cref="EventBuilder.Build"/> returns event
        /// when some trouble coins are supplied,
        /// and random generates non-zero.
        /// </summary>
        /// <param name="troubleCoins">The number of trouble coins supplied to the method.</param>
        /// <param name="generated">The random value to be generated.</param>
        /// <param name="expectedEventLevel">The expected level event.</param>
        [TestCase(1, 1, EventLevel.Low)]
        [TestCase(2, 1, EventLevel.Low)]
        [TestCase(2, 2, EventLevel.Medium)]
        [TestCase(3, 1, EventLevel.Low)]
        [TestCase(3, 2, EventLevel.Medium)]
        [TestCase(3, 3, EventLevel.High)]
        public void Test_Build_WhenRandomGeneratesNonZero(int troubleCoins, int generated, EventLevel expectedEventLevel)
        {
            // Arrange
            CreateEventDto createEventDto = new() { GeneratorId = _generatorId, TroubleCoins = troubleCoins };

            _randomMock.Setup(x => x.Next(0, It.IsAny<int>())).Returns(generated);

            // Act
            Event? result = _eventBuilder.Build(createEventDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.GeneratorId, Is.EqualTo(_generatorId));
                Assert.That(result?.EventLevel, Is.EqualTo(expectedEventLevel));

                Assert.That(_loggerMock.Invocations, Has.Count.EqualTo(1));

                _eventRepositoryMock.Verify(x => x.Create(It.IsAny<Event>()), Times.Once);
                _eventRepositoryMock.VerifyNoOtherCalls();

                _randomMock.Verify(x => x.Next(0, troubleCoins + 1), Times.Once);
                _randomMock.VerifyNoOtherCalls();
            });
        }

        #endregion
    }
}
