using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NUnit.Framework.Internal;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.Services.Exceptions;
using SpaceShip.WebAPI.Controllers;
using SpaceShip.WebAPI.Mappers;
using SpaceShip.WebAPI.Models;

namespace SpaceShip.UnitTest.Controllers;

public class SpaceShipControllerTests
{
    private readonly Guid _createdShipId = Guid.NewGuid();
    private IMapper _mapper;
    private Mock<IShipService> _mockService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _mapper = new Mapper(
            new MapperConfiguration(
                static cfg =>
                {
                    cfg.AddProfile<SpaceShipMappingProfile>();
                }));
    }

    [SetUp]
    public void SetUp()
    {
        _mockService = new Mock<IShipService>();
    }

    [Test]
    public void CreateShip_MappingServiceAnswer()
    {
        _mockService.Setup(svc => svc.CreateShip()).Returns(CreateShip(_createdShipId));
        var controller = new SpaceShipController(_mockService.Object, _mapper);

        var response = controller.Create();

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.InstanceOf<Created<SpaceShipCreateResponse>>());
            Assert.That(((Created<SpaceShipCreateResponse>)response).Value.Id, Is.EqualTo(_createdShipId));
        });
    }

    [Test]
    public void GetShip_WhenOk()
    {
        _mockService.Setup(svc => svc.GetShip(_createdShipId)).Returns(CreateShip(_createdShipId));
        var controller = new SpaceShipController(_mockService.Object, _mapper);

        var response = controller.Get(_createdShipId);

        Assert.Multiple(() =>
        {
            Assert.That(response, Is.InstanceOf<Ok<SpaceShipMetricResponse>>());
            Assert.That(((Ok<SpaceShipMetricResponse>)response).Value.Id, Is.EqualTo(_createdShipId));
            Assert.That(((Ok<SpaceShipMetricResponse>)response).Value.Step, Is.EqualTo(0));
        });
    }

/*
    [Test]
    public void GetShip_WhenNotFound()
    {
        _mockService.Setup(svc => svc.GetShip(Guid.Empty)).Throws(new NotFoundException("test id not found"));
        var controller = new SpaceShipController(_mockService.Object, _mapper);

        var response = controller.Get(Guid.Empty);

        Assert.That(response, Is.InstanceOf<NotFound>());
    }
*/

    private static ShipDTO CreateShip(Guid id)
    {
        return new ShipDTO
        {
            Id = id,
            Step = 0
        };
    }
}
