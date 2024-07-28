using AutoMapper;
using GameController.Database.Interfaces;
using GameController.Database.Models;
using GameController.Services.Exceptions;
using GameController.Services.Models.User;
using GameController.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace GameController.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ILogger<UserService>> _loggerMock;
        private Mock<IMapper> _mapperMock;


        [SetUp]
        public void SetupUserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        public void SetupCreateUser(CreateUserDto createUserDto, User user)
        {
            _mapperMock.Setup(m => m.Map<User>(createUserDto)).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Create(user)).Returns(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(new UserDto());
        }

        [Test]
        public void GetUser_ReturnsUserDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Name = "Test User", PasswordHash = "hashedPassword" };
            var userDto = new UserDto { Id = userId, Name = "Test User" };
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns(user);
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(new UserDto { Id = userId, Name = user.Name });

            // Act
            var result = _userService.GetUser(userId);

            // Assert
            //  Assert.Multiple(() => {Assert.That(result,)});
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userDto.Id));
            Assert.That(result.Name, Is.EqualTo(userDto.Name));
            _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);
        }

        [Test]
        public void GetUser_ThrowsNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.Get(userId)).Returns((User)null);

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _userService.GetUser(userId));
            _userRepositoryMock.Verify(repo => repo.Get(userId), Times.Once);
        }
    }
}
