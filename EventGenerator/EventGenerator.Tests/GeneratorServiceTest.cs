using AutoMapper;
using EventGenerator.Database.Interfaces;
using EventGenerator.Database.Models;
using EventGenerator.Services.Exceptions;
using EventGenerator.Services.Interfaces;
using EventGenerator.Services.Mappers;
using EventGenerator.Services.Models.Generator;
using EventGenerator.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace EventGenerator.Tests
{
    /// <summary>
    /// Tests for <see cref="GeneratorService"/>.
    /// </summary>
    [TestFixture]
    public class GeneratorServiceTest
    {
        #region private fields

        private readonly Guid _shipId = Guid.NewGuid();
        private readonly Guid _generatorId = Guid.NewGuid();

        private GeneratorService _generatorService;

        private Mock<IGeneratorRepository> _generatorRepositoryMock;
        private Mock<IEventService> _eventServiceMock;
        private Mock<ILogger<GeneratorService>> _loggerGeneratorMock;

        private IMapper _mapper;

        #endregion

        #region setup

        /// <summary>
        /// One-time setup.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _generatorRepositoryMock = new Mock<IGeneratorRepository>();
            _eventServiceMock = new Mock<IEventService>();
            _loggerGeneratorMock = new Mock<ILogger<GeneratorService>>();

            _mapper = new Mapper(
                new MapperConfiguration(
                    static cfg =>
                    {
                        cfg.AddProfile<GeneratorMapper>();
                    }));

            _generatorService = new GeneratorService(
                _generatorRepositoryMock.Object,
                _eventServiceMock.Object,
                _loggerGeneratorMock.Object,
                _mapper);
        }

        /// <summary>
        /// Setup for every test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _generatorRepositoryMock.Invocations.Clear();
            _eventServiceMock.Invocations.Clear();
            _loggerGeneratorMock.Invocations.Clear();
        }

        #endregion

        #region tests for CreateGenerator

        /// <summary>
        /// Test that <see cref="GeneratorService.CreateGenerator"/>
        /// creates a generator.
        /// </summary>
        [Test]
        public void Test_CreateGenerator()
        {
            // Arrange
            CreateGeneratorDto serviceRequest = new() { ShipId = _shipId };

            _generatorRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<Generator>()))
                .Returns(new Generator() { Id = _generatorId, ShipId = _shipId });

            // Act
            GeneratorDto result = _generatorService.CreateGenerator(serviceRequest);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.GeneratorId, Is.EqualTo(_generatorId));
                Assert.That(result.ShipId, Is.EqualTo(serviceRequest.ShipId));
                Assert.That(result.TroubleCoins, Is.EqualTo(0));

                _generatorRepositoryMock.Verify(
                    repo => repo.Create(
                        It.Is<Generator>(g =>
                            g.Id == Guid.Empty &&
                            g.ShipId == _shipId &&
                            g.TroubleCoins == 0)),
                    Times.Once);
                _generatorRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region tests for GetGenerator

        /// <summary>
        /// Test that <see cref="GeneratorService.GetGenerator"/> returns
        /// the correctly mapped generator when it exists.
        /// </summary>
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Test_GetGenerator_WhenGeneratorExists(int troubleCoins)
        {
            // Arrange
            Generator generator = new() { Id = _generatorId, ShipId = _shipId, TroubleCoins = troubleCoins };

            _generatorRepositoryMock.Setup(repo => repo.Get(_generatorId, It.IsAny<bool>())).Returns(generator);

            // Act
            var result = _generatorService.GetGenerator(_generatorId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.GeneratorId, Is.EqualTo(_generatorId));
                Assert.That(result.ShipId, Is.EqualTo(_shipId));
                Assert.That(result.TroubleCoins, Is.EqualTo(troubleCoins));

                _generatorRepositoryMock.Verify(repo => repo.Get(_generatorId, It.IsAny<bool>()), Times.Once);

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        /// <summary>
        /// Test that <see cref="GeneratorService.GetGenerator"/>
        /// throws when the generator is not found.
        /// </summary>
        public void Test_GetGenerator_WhenGeneratorNotFound()
        {
            // Arrange
            _generatorRepositoryMock.Setup(repo => repo.Get(_generatorId, It.IsAny<bool>())).Returns((Generator?)null);

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotFoundException>(() => _generatorService.GetGenerator(_generatorId));

                _generatorRepositoryMock.Verify(repo => repo.Get(_generatorId, It.IsAny<bool>()), Times.Once);
                _generatorRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion

        #region tests for AddTroubleCoin

        /// <summary>
        /// Test that <see cref="GeneratorService.AddTroubleCoin"/> increases
        /// the trouble coins and returns the correctly mapped generator
        /// when it exists.
        /// </summary>
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Test_AddTroubleCoin_WhenGeneratorExists(int troubleCoins)
        {
            // Arrange
            Generator generator = new() { Id = _generatorId, ShipId = _shipId, TroubleCoins = troubleCoins };

            _generatorRepositoryMock.Setup(repo => repo.Get(_generatorId, It.IsAny<bool>())).Returns(generator);
            _generatorRepositoryMock.Setup(repo => repo.Update(generator));

            // Act
            GeneratorDto result = _generatorService.AddTroubleCoin(_generatorId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.TroubleCoins, Is.EqualTo(troubleCoins + 1));

                _generatorRepositoryMock.Verify(repo => repo.Get(_generatorId, It.IsAny<bool>()), Times.Once);
                _generatorRepositoryMock.Verify(repo => repo.Update(generator), Times.Once);
                _generatorRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(1));
            });
        }

        /// <summary>
        /// Test that <see cref="GeneratorService.AddTroubleCoin"/>
        /// throws when the generator is not found.
        /// </summary>
        public void Test_AddTroubleCoin_WhenGeneratorNotFound()
        {
            // Arrange
            _generatorRepositoryMock.Setup(repo => repo.Get(_generatorId, It.IsAny<bool>())).Returns((Generator?)null);

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotFoundException>(() => _generatorService.AddTroubleCoin(_generatorId));

                _generatorRepositoryMock.Verify(repo => repo.Get(_generatorId, It.IsAny<bool>()), Times.Once);
                _generatorRepositoryMock.VerifyNoOtherCalls();

                Assert.That(_loggerGeneratorMock.Invocations, Has.Count.EqualTo(2));
            });
        }

        #endregion
    }
}
