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

    [Test]
    public void GetShip_WhenOk()
    {
        var repository = new MockSpaceShipRepository();
        var ship = repository.Create();
        Guid id = ship.Id;

        var result = repository.FindById(id);

        Assert.That(result.Id, Is.EqualTo(id));
    }

    [Test]
    public void NextDay_WhenOk()
    {
        var repository = new MockSpaceShipRepository();
        var ship = repository.Create();
        Guid id = ship.Id;
        int initStep = ship.Step;

        var result = repository.NextDay(id);

        Assert.That(result.Step, Is.EqualTo(initStep + 1));
    }
}
