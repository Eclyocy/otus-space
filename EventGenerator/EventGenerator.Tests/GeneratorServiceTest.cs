using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Models.Generator;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EventGenerator.Tests
{
    [TestFixture]
    public class GeneratorServiceTest
    {
        #region private fields

        private GeneratorService _generatorService;
        private Mock<IGeneratorRepository> _generatorRepositoryMock;
        private Mock<IEventService> _eventServiceMock;
        private Mock<ILogger<GeneratorService>> _loggerGeneratorMock;
        private Mock<IMapper> _mapperMock;
        private readonly Guid shipId = Guid.NewGuid();
        private readonly Guid generatorId = Guid.NewGuid();
        private readonly int troubleCoins = new Random().Next(0, 3);

        #endregion

        #region setup
        [SetUp]
        public void SetupGeneratorServiceTests()
        {
            _generatorRepositoryMock = new Mock<IGeneratorRepository>();
            _eventServiceMock = new Mock<IEventService>();
            _loggerGeneratorMock = new Mock<ILogger<GeneratorService>>();
            _mapperMock = new Mock<IMapper>();
            _generatorService = new GeneratorService(_generatorRepositoryMock.Object, _eventServiceMock.Object, _loggerGeneratorMock.Object, _mapperMock.Object);
        }

        #endregion

        #region tests for CreateGenerator

        [Test]
        public void CreateGenerator_ReturnGeneratorDto_WhenGeneratorCorrect()
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

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for GetGenerator

        [Test]
        public void GetGenerator_ReturnsGeneratorDto_WhenGeneratorExists()
        {
            // Arrange
            var generator = new Generator { Id = generatorId, ShipId = shipId, TroubleCoins = troubleCoins };
            var generatorDto = new GeneratorDto { ShipId = shipId, GeneratorId = generatorId, TroubleCoins = troubleCoins };
            _generatorRepositoryMock.Setup(repo => repo.Get(generatorId, false)).Returns(generator);
            _mapperMock.Setup(m => m.Map<GeneratorDto>(generator)).Returns(new GeneratorDto { ShipId = generator.ShipId, GeneratorId = generatorId, TroubleCoins = generator.TroubleCoins });

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

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(1));
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

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion
    }
}
