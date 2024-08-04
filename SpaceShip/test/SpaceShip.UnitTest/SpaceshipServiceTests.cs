using AutoMapper;
using MockSpaceShip.Repository;
// using Moq;
using NUnit.Framework.Internal;
// using SpaceShip.Domain.DTO;
// using SpaceShip.Domain.Interfaces;
using SpaceShip.Domain.Mappers;


namespace SpaceShip.UnitTest.Services;

public class SpaceShipControllerTests
{
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(
            new MapperConfiguration(
                static cfg =>
                {
                    cfg.AddProfile<SpaceShipModelMappingProfile>();
                }
        ));
    }

    [Test]
    public void CreateShip_WhenOk()
    {
        // var mock  = new Mock<IShipRepository>();
        // mock.Setup(repo => repo.Create()).Returns(CreateShip()); <-- пока не слили нарабатотки репозитория
        var repository = new MockSpaceShipRepository();

        var ship = repository.Create();

        Assert.That(ship.Id, Is.InstanceOf<Guid>());
        Assert.That(ship.Step, Is.EqualTo(0));
    }

    // private static SpaceShipModelDto CreateShip()
    // {
    //     return new SpaceShipModelDto();
    // }
}
