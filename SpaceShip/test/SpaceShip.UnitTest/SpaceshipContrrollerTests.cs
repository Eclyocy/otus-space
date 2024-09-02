using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using NUnit.Framework.Internal;
using SpaceShip.Service.Contracts;
using SpaceShip.Service.Interfaces;
using SpaceShip.WebAPI.Controllers;
using SpaceShip.WebAPI.Mappers;
using SpaceShip.WebAPI.Models;

namespace SpaceShip.UnitTest.Controllers;

public class SpaceShipControllerTests
{
    private readonly Guid _createdShipId = Guid.NewGuid();
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(
            new MapperConfiguration(
                static cfg =>
                {
                    cfg.AddProfile<SpaceShipMappingProfile>();
                }
        ));
    }

    [Test]
    public void CreateShip_MappingServiceAnswer()
    {
        var mock  = new Mock<IShipService>();
        mock.Setup(svc => svc.CreateShip()).Returns(CreateShip(_createdShipId));
        var controller = new SpaceShipController(mock.Object, _mapper);

        var response = controller.Create();
        
        Assert.IsInstanceOf<Created<SpaceShipCreateResponse>>(response);
        Assert.That(((Created<SpaceShipCreateResponse>)response).Value.Id, Is.EqualTo(_createdShipId));
    }

    [Test]
    public void GetShip_WhenOk()
    {
        var mock  = new Mock<IShipService>();
        mock.Setup(svc => svc.GetShip(_createdShipId)).Returns(CreateShip(_createdShipId));
        var controller = new SpaceShipController(mock.Object, _mapper);

        var response = controller.Get(_createdShipId);
        
        Assert.IsInstanceOf<Ok<SpaceShipMetricResponse>>(response);
        Assert.That(((Ok<SpaceShipMetricResponse>)response).Value.Id, Is.EqualTo(_createdShipId));
        Assert.That(((Ok<SpaceShipMetricResponse>)response).Value.Step, Is.EqualTo(0));
    }

    [Test]
    public void GetShip_WhenNotFound()
    {
        var mock  = new Mock<IShipService>();
        mock.Setup(svc => svc.GetShip(Guid.Empty)).Throws<KeyNotFoundException>();
        var controller = new SpaceShipController(mock.Object, _mapper);

        var response = controller.Get(Guid.Empty);
        
        Assert.IsInstanceOf<NotFound>(response);
    }

    private static SpaceShipDTO CreateShip(Guid id)
    {
        return new SpaceShipDTO
        {
            Id = id,
            Step = 0
        };
    }
}
