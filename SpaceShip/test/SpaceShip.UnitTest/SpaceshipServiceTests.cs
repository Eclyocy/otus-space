using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using SpaceShip.Domain.Entities;
using SpaceShip.Domain.Interfaces;
using SpaceShip.Service.Builder.Abstractions;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Service.Mappers;
using SpaceShip.Service.Services;
using SpaceShip.Services.Exceptions;
using SpaceShip.WebAPI.Mappers;

namespace SpaceShip.UnitTest.Services;

public class SpaceShipServiceTests
{
    private IMapper _mapper;
    private IShipRepository _shipRepository;
    private IResourceService _resourceService;
    private IShipBuilder _shipBuilder;
    private ILogger<ShipService> _logger;
    private ShipService _service;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mapper = new Mapper(
            new MapperConfiguration(
                static cfg =>
                {
                    cfg.AddProfile<SpaceShipMappingProfile>();
                    cfg.AddProfile<ShipMappingProfile>();
                    cfg.AddProfile<ResourceMappingProfile>();
                }));

        var mock = new Mock<IShipRepository>();
        mock.Setup(repo => repo.Create(It.IsAny<bool>())).Returns(new Ship());
        mock.Setup(repo => repo.Create(It.IsAny<Ship>(), It.IsAny<bool>())).Returns((Ship ship, bool saveChanges) => { return ship; });
        mock.Setup(repo => repo.Get(It.IsAny<Guid>())).Returns<Guid>(static (id) =>
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            Ship ship = new ();
            ship.Id = id;
            return ship;
        });
        _shipRepository = mock.Object;

        _resourceService = new Mock<IResourceService>().Object;

        var moqShipBuilder = new Mock<IShipBuilder>();
        moqShipBuilder.Setup(builder => builder.Build()).Returns(new Ship());
        _shipBuilder = moqShipBuilder.Object;

        _logger = Mock.Of<ILogger<ShipService>>();
    }

    [SetUp]
    public void Setup()
    {
        _service = new ShipService(_resourceService, _shipRepository, _mapper, _logger, _shipBuilder);
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
            Assert.That(result, Is.InstanceOf<ShipDTO>());
            Assert.That(result.Id, Is.EqualTo(id));
        });
    }

    [Test]
    public void GetShip_WhenNotFound()
    {
        Assert.Throws<NotFoundException>(() => _service.GetShip(Guid.Empty));
    }
}
