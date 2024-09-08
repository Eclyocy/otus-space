using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Mappers;
using SpaceShip.Domain.Model;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Implementation;
using SpaceShip.Service.Mappers;
using SpaceShip.Services.Exceptions;

namespace SpaceShip.UnitTest.Services;

public class SpaceShipServiceTests
{
    private IMapper _mapper;
    private ISpaceshipRepository _repository;
    private ILogger<SpaceShipService> _logger;
    private SpaceShipService _service;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mapper = new Mapper(
            new MapperConfiguration(
                static cfg =>
                {
                    cfg.AddProfile<SpaceShipModelMappingProfile>();
                    cfg.AddProfile<ProblemModelMappingProfile>();
                    cfg.AddProfile<ResourceModelMappingProfile>();
                    cfg.AddProfile<ResourceStateModelMappingProfile>();
                    cfg.AddProfile<ResourceTypeModelMappingProfile>();
                }));

        var mock = new Mock<ISpaceshipRepository>();
        mock.Setup(repo => repo.Create()).Returns(new Ship());
        mock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns<Guid>((id) =>
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return new Ship
            {
                Id = id
            };
        });
        _repository = mock.Object;

        _logger = Mock.Of<ILogger<SpaceShipService>>();
    }

    [SetUp]
    public void Setup()
    {
        _service = new SpaceShipService(_repository, _mapper, _logger);
    }

    [Test]
    public void CreateShip_WhenOk()
    {
        var ship = _service.CreateShip();

        Assert.Multiple(() =>
        {
            Assert.That(ship.Id, Is.InstanceOf<Guid>());
            Assert.That(ship.Step, Is.EqualTo(0));
        });
    }

    [Test]
    public void GetShip_WhenOk()
    {
        var id = Guid.NewGuid();
        var result = _service.GetShip(id);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<SpaceShipDTO>());
            Assert.That(result.Id, Is.EqualTo(id));
        });
    }

    [Test]
    public void GetShip_WhenNotFound()
    {
        Assert.Throws<NotFoundException>(() => _service.GetShip(Guid.Empty));
    }
}
